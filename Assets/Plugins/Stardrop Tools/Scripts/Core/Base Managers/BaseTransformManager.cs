
namespace StardropTools
{
    /// <summary>
    /// BaseTransform object class with the IManager interface. Still use Initialize() and LateInitialze() for class setup
    /// <para>Note : NOT a singleton!</para>
    /// </summary>
    public abstract class BaseTransformManager : BaseTransform, IManager
    {
        public bool canUpdate { get; protected set; }

        public void InitializeManager() => Initialize();

        public void LateInitializeManager() => LateInitialize();

        public override void Initialize()
        {
            base.Initialize();
            EventSubscription();
        }

        /// <summary>
        /// Subscribe to events on this method. Helps to organize code
        /// </summary>
        protected abstract void EventSubscription();
        public abstract void GameStateChanged(GameState newGameState, GameState previousGameState);
    }
}