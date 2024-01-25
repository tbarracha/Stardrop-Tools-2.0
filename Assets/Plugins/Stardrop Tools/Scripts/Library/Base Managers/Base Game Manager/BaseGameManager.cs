
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    public abstract class BaseGameManager : ManagerInitializer
    {
        #region Singleton
        /// <summary>
        /// The instance.
        /// </summary>
        private static BaseGameManager instance;


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static BaseGameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BaseGameManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(BaseGameManager).Name;
                        instance = obj.AddComponent<BaseGameManager>();
                    }
                }
                return instance;
            }
        }

        public void SingletonInitialization()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

            else
                Destroy(gameObject);
        }
        #endregion // singleton

        [SerializeField] protected bool                 isSingleton;
        [SerializeField] protected string               currentGameStateName = string.Empty;
        [SerializeField] protected BaseScriptableObject[] baseScriptableObjects;
        
        protected GameState        currentGameState;
        protected List<GameState>  gameStates;

        public static class DefaultGameStateNames
        {
            public const string Initialization = "Initialization";
            public const string MainMenu = "Main Menu";
            public const string Play = "Play";
            public const string Paused = "Paused";
            public const string PlayEnd = "Play End";
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (initializeAt != InitializeComponentIn.Awake)
                initializeAt = InitializeComponentIn.Awake;
        }
#endif

        #region Initialization

        public override void Initialize()
        {
            base.Initialize();

            if (isSingleton)
                SingletonInitialization();

            CreateGameStates();

            EventFlow();
            InitializeManagers();
            LateInitialize();

            Utilities.Initialize(baseScriptableObjects);
        }

        protected virtual void CreateGameStates()
        {
            gameStates = new List<GameState>();

            RegisterState(DefaultGameStateNames.Initialization);
            RegisterState(DefaultGameStateNames.MainMenu);
            RegisterState(DefaultGameStateNames.Play);
            RegisterState(DefaultGameStateNames.Paused);
            RegisterState(DefaultGameStateNames.PlayEnd);

            currentGameState = gameStates[0];
            currentGameStateName = currentGameState.StateName;
        }

        protected virtual void EventFlow()
        {
            BaseEventManager.InitializationEvents.OnPoolsInitialized.AddListener(() => ChangeGameState(1));

            BaseEventManager.GameStateEvents.OnRequestGameStateChange.AddListener(OnGameStateRequested);
            BaseEventManager.GameStateEvents.OnRequestGameStateIDChange.AddListener(OnGameStateRequested);
            BaseEventManager.GameStateEvents.OnRequestGameStateNameChange.AddListener(OnGameStateRequested);
        }

        #endregion // Initialization


        #region Public API

        public bool RegisterState(GameState state)
        {
            if (gameStates == null)
            {
                gameStates = new List<GameState>();
            }

            if (gameStates.Count == 0)
            {
                gameStates.Add(state);
                SetStateID(state, 0);
                return true;
            }

            for (int i = 0; i < gameStates.Count; i++)
            {
                if (state == gameStates[i])
                {
                    return false;
                }
            }

            gameStates.Add(state);
            RefreshStateIDs();
            return true;
        }

        public bool RegisterState(string stateName)
        {
            return RegisterState(new GameState(stateName));
        }

        public bool UnregisterState(GameState state)
        {
            if (gameStates == null)
            {
                gameStates = new List<GameState>();
            }

            if (gameStates.Count == 0)
            {
                return false;
            }

            for (int i = 0; i < gameStates.Count; i++)
            {
                if (state.StateName.Equals(gameStates[i]))
                {
                    gameStates.RemoveAt(i);
                    RefreshStateIDs();
                    return true;
                }
            }

            return false;
        }

        public bool UnregisterState(string stateName)
        {
            return UnregisterState(new GameState(stateName));
        }

        public bool RequestGameStateChange(GameState gameState)
        {
            if (gameStates.Contains(gameState))
            {
                ChangeGameState(gameState);
                return true;
            }

            return false;
        }

        public bool RequestGameStateChange(string stateName)
        {
            foreach (GameState state in gameStates)
            {
                if (state.StateName.Equals(stateName))
                {
                    ChangeGameState(state);
                    return true;
                }
            }

            return false;
        }


        public bool RequestGameStateChange(int stateID)
        {
            return RequestGameStateChange(gameStates[stateID]);
        }

        #endregion // API



        #region Private Methods - Internal Workings

        private void SetStateID(GameState state, int id) => state.StateID = id;

        private void RefreshStateIDs()
        {
            if (gameStates == null || gameStates.Count == 0)
                return;

            int index = 0;
            foreach (GameState state in gameStates)
            {
                SetStateID(state, index++);
            }
        }

        protected virtual bool ChangeGameState(GameState targetGameState)
        {
            if (targetGameState == null)
                return false;

            if (currentGameState != null && currentGameState.Equals(targetGameState))
                return false;

            if (currentGameState != null)
                currentGameState.ExitState();

            var prevState    = currentGameState;
            currentGameState = targetGameState;
            currentGameState.EnterState(prevState);

            currentGameStateName = currentGameState.StateName;
            BaseEventManager.GameStateEvents.OnGameStateChanged?.Invoke(currentGameState, prevState);

            if (prevState != null)
                Debug.Log($"Game State Changed from {prevState.StateName} to {currentGameState.StateName}");
            else
                Debug.Log($"Game State Changed to {currentGameState.StateName}");

            return true;
        }

        protected virtual bool ChangeGameState(int stateID)
        {
            return ChangeGameState(gameStates[stateID]);
        }

        private void OnGameStateRequested(GameState targetGameState)
        {
            RequestGameStateChange(targetGameState);
        }

        private void OnGameStateRequested(string stateName)
        {
            RequestGameStateChange(stateName);
        }

        private void OnGameStateRequested(int stateID)
        {
            RequestGameStateChange(stateID);
        }

        #endregion // private methods
    }
}
