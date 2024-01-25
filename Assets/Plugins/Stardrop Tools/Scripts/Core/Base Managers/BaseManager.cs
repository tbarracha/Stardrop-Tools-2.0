
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// BaseComponent class with the IManager interface. Still use Initialize() and LateInitialze() for class setup
    /// <para>Note : CAN be a Singleton but Requires Initialization!</para>
    /// </summary>
    public abstract class BaseManager<T> : BaseComponent, IManager, ISingleton<T> where T : MonoBehaviour
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

            else
            {
                Destroy(gameObject);
            }
        }
        #endregion // singleton

        [SerializeField] protected bool isSingleton;

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
            BaseEventManager.GameStateEvents.OnGameStateChanged.AddListener(GameStateChanged);
        }

        public abstract void GameStateChanged(GameState newGameState, GameState previousGameState);

        /*
        public override void GameStateChanged(GameState newGameState, GameState previousGameState)
        {
            string stateName = newGameState.StateName;

            switch (stateName)
            {
                case BaseGameManager.DefaultGameStateNames.Initialization:
                    break;

                case BaseGameManager.DefaultGameStateNames.MainMenu:
                    break;

                case BaseGameManager.DefaultGameStateNames.Play:
                    break;

                case BaseGameManager.DefaultGameStateNames.Paused:
                    break;

                case BaseGameManager.DefaultGameStateNames.PlayEnd:
                    break;
            }
        }
        */
    }
}