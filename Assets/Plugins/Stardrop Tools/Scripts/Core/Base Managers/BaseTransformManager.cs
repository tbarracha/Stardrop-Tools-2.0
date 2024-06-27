
using StardropTools.GameStateManagement;
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// BaseTransform object class with the IManager interface. Still use Initialize() and LateInitialze() for class setup
    /// <para>Note : NOT a singleton!</para>
    /// </summary>
    public abstract class BaseTransformManager<T> : BaseTransform, IManager, ISingleton<T> where T : MonoBehaviour
    {
        #region Singleton
        /// <summary>
        /// The instance.
        /// </summary>
        protected static T instance;


        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.name = typeof(T).Name;
                        instance = obj.AddComponent<T>();
                    }

                    if (instance != null)
                    {
                        DontDestroyOnLoad(instance);
                    }
                }
                return instance;
            }
        }

        public void SingletonInitialization()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            //else
            //{
            //    Destroy(gameObject);
            //}
        }
        #endregion // singleton

        [SerializeField] protected bool isSingleton;

        public bool canUpdate { get; protected set; }

        public void InitializeManager() => Initialize();

        public void LateInitializeManager() => LateInitialize();

        public override void Initialize()
        {
            base.Initialize();

            if (isSingleton)
            {
                SingletonInitialization();
            }

            EventSubscription();
        }



        /// <summary>
        /// Subscribe to events on this method. Helps to organize code
        /// </summary>
        protected virtual void EventSubscription()
        {
            BaseGameEventManager.GameStateEvents.OnGameStateChanged.Subscribe(GameStateChanged);
        }

        public abstract void GameStateChanged(GameState currentState, GameState prevGameState);

        /*
        public override void GameStateChanged(GameState currentState, GameState prevGameState)
        {
            switch (currentState.TryGetStandardGameState())
            {
                case StandardGameStates.Initializing:
                    break;

                case StandardGameStates.MainMenu:
                    break;

                case StandardGameStates.Play:
                    break;

                case StandardGameStates.Paused:
                    break;

                case StandardGameStates.GameOver:
                    break;

                case StandardGameStates.Loading:
                    break;

                case StandardGameStates.LevelTransition:
                    break;

                case StandardGameStates.Settings:
                    break;

                case StandardGameStates.Exit:
                    break;
            }
        }
        */
    }
}