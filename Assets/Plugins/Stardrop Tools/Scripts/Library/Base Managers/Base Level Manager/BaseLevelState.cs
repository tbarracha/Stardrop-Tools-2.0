
namespace StardropTools
{
    [System.Serializable]
    public abstract class BaseLevelState : IManagerState
    {
        public string StateName { get; }
        public int    StateID { get; set; }

        public static int LevelStateCount { get; private set; } = 0;


        public BaseLevelState(string name)
        {
            StateName   = name;
            StateID     = LevelStateCount;
            StateCreated(this);
        }

        public int StateCreated(IManagerState gameState)
        {
            if (gameState == null || gameState is BaseLevelState == false)
                return 0;

            LevelStateCount++;
            return LevelStateCount;
        }

        public int GetStateID()      => StateID;

        public string GetStateName() => GetType().Name;

        public virtual void EnterState(IManagerState previousState)
        {
            if (previousState == null || previousState is BaseLevelState == false)
                return;
        }

        public virtual void ExitState()
        {
            
        }
    }
}
