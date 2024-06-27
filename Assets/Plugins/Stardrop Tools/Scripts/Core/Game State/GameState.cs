
namespace StardropTools.GameStateManagement
{
    [System.Serializable]
    public class GameState : IStateId, IStateEnter, IStateEnter<GameState>, IStateExit
    {
        [UnityEngine.SerializeField] protected int stateId;
        [UnityEngine.SerializeField] protected string stateName;

        public int StateId { get => stateId; set => stateId = value; }
        public int PreviousStateId { get; set; }
        public string StateName { get => stateName; set => stateName = value; }

        public StandardGameStates TryGetStandardGameState()
        {
            try
            {
                return (StandardGameStates)StateId;
            }
            catch
            {
                return StandardGameStates.NotFound;
            }
        }

        public GameState(string stateName)
        {
            this.stateId = -1;
            this.stateName = stateName;
        }

        public GameState(int stateId, string stateName)
        {
            this.stateId = stateId;
            this.stateName = stateName;
        }

        public void EnterState()
        {
            //UnityEngine.Debug.Log($"Entered State {StateName}");
        }

        public virtual void EnterState(GameState previousState)
        {
            //UnityEngine.Debug.Log($"Entered State: {StateName}, from State: {previousState}");
        }

        public virtual void ExitState()
        {
            // Implementation
        }

        public override bool Equals(object obj)
        {
            return obj is GameState state &&
                   StateId == state.StateId &&
                   StateName.Equals(state.StateName);
        }

        public override string ToString()
        {
            return $"Game State {{ ID: {StateId}, Name: {StateName} }}";
        }
    }
}
