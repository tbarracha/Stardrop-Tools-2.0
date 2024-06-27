
namespace StardropTools
{
    public interface IState : IStateId, IStateEnter, IStateExit, IStateUpdate
    {

    }

    public interface IState<T> : IStateId, IStateEnter<T>, IStateExit<T>, IStateUpdate<T>
    {

    }
}
