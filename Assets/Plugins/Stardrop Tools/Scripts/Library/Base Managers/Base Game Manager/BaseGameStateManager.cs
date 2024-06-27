using StardropTools.GameStateManagement;
using UnityEngine;

namespace StardropTools
{
    public abstract class BaseGameStateManager : ManagerInitializer, IGameStateMachine
    {
        #region Singleton
        /// <summary>
        /// The instance.
        /// </summary>
        protected static BaseGameStateManager instance;


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static BaseGameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<BaseGameStateManager>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(BaseGameStateManager).Name;
                        instance = obj.AddComponent<BaseGameStateManager>();
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

        [SerializeField] protected bool isSingleton;
        [SerializeField] protected bool debugStateChange;
        [SerializeField] protected string currentGameStateName = string.Empty;
        [SerializeField] protected BaseScriptableObject[] baseScriptableObjects;
        [SerializeField] protected GameStateMachine gameStateMachine;

        bool canChange = true;

        public GameState CurrentState => gameStateMachine.CurrentState;

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (initializeAt != InitializeComponentIn.Awake)
                initializeAt = InitializeComponentIn.Awake;
        }
#endif



        // Initialization
        // ---------------------------------------------------------------------------------------
        public override void Initialize()
        {
            base.Initialize();

            if (isSingleton)
                SingletonInitialization();

            gameStateMachine = new GameStateMachine();
            CreateGameStates();

            EventFlow();
            Utilities.Initialize(baseScriptableObjects);
            InitializeManagers();

            LateInitialize();
        }

        protected virtual void EventFlow()
        {
            gameStateMachine.OnGameStateChanged.Subscribe(GameStateChanged);
            //BaseEventManager.InitializationEvents.OnPoolsInitialized.AddListener(() => ChangeGameState(2));

            BaseGameEventManager.GameStateEvents.OnRequestGameStateChange.Subscribe((state) => ChangeGameState(state));
            BaseGameEventManager.GameStateEvents.OnRequestGameStateChangeById.Subscribe((stateId) => ChangeGameState(stateId));
            BaseGameEventManager.GameStateEvents.OnRequestGameStateChangeByName.Subscribe((stateName) => ChangeGameState(stateName));

            BaseGameEventManager.GameStateEvents.OnGameOverRequest.Subscribe(GameOver);
        }


        // Game State
        // ---------------------------------------------------------------------------------------
        protected abstract void CreateGameStates();
        
        protected virtual void GameStateChanged(GameState currentState, GameState prevState)
        {
            StartChangeTimer();

            if (debugStateChange)
            {
                Debug.Log($"Game State Changed to: {currentState.StateName}, from: {prevState?.StateName}");
            }

            currentGameStateName = currentState.StateName;
            BaseGameEventManager.GameStateEvents.OnGameStateChanged?.Invoke(currentState, prevState);
        }

        public bool ChangeGameState(GameState state)
        {
            if (!canChange)
                return false;

            return gameStateMachine.ChangeGameState(state);
        }

        public bool ChangeGameState(string stateName)
        {
            if (!canChange)
                return false;

            return gameStateMachine.ChangeGameState(stateName);
        }

        public bool ChangeGameState(int stateId)
        {
            if (!canChange)
                return false;

            return gameStateMachine.ChangeGameState(stateId);
        }

        public bool ChangeGameState(StandardGameStates standardGameState)
        {
            if (!canChange)
                return false;

            return gameStateMachine.ChangeGameState(standardGameState);
        }


        public void GameOver(GameOverCause cause)
        {
            ChangeGameState(StandardGameStates.GameOver);
            BaseGameEventManager.GameStateEvents.OnGameOver?.Invoke(cause);
        }

        void StartChangeTimer()
        {
            canChange = false;
            new Timer(.05f).Play(ResetCanChange);
        }

        void ResetCanChange()
        {
            canChange = true;
        }
    }
}
