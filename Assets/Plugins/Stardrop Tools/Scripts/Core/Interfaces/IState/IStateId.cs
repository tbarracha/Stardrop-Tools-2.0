
namespace StardropTools
{
    public interface IStateId
    {
        int StateId { get; }
        string StateName { get; }
    }

    public interface IStateId<T>
    {
        int StateId { get; }
        string StateName { get; }
    }
}
