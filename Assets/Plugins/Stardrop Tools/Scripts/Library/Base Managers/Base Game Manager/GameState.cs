
namespace StardropTools
{
    [System.Serializable]
    public class GameState : IManagerState
    {
        [UnityEngine.SerializeField]
        protected int stateID;

        [UnityEngine.SerializeField]
        protected string stateName;


        public int StateID { get => stateID; set => stateID = value; }

        public string StateName => stateName;


        public GameState(string stateName)
        {
            this.stateID = 0;
            this.stateName = stateName;

            BaseGameManager.Instance.RegisterState(this);
        }

        public GameState(int stateID, string stateName)
        {
            this.stateID = stateID;
            this.stateName = stateName;

            BaseGameManager.Instance.RegisterState(this);
        }

        public virtual void EnterState(IManagerState previousState)
        {
            // Implementation
        }

        public virtual void ExitState()
        {
            // Implementation
        }

        public override bool Equals(object obj)
        {
            return obj is GameState state &&
                   StateID == state.StateID &&
                   StateName.Equals(state.StateName);
        }

        public override string ToString()
        {
            return $"Game State{{ ID: {StateID}, Name: {StateName} }}";
        }
    }
}
