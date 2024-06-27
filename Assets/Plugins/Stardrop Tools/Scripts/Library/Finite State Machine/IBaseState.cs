
namespace StardropTools.FiniteStateMachine
{
    public interface IBaseState : IState
    {
        public void SetID(int stateID);

        public void Initialize(FiniteStateMachine stateMachine, int stateID);
        public void HandleInput();
    }
}