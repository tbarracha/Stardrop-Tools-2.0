
namespace StardropTools
{
    public interface IStoppableCallback : IStoppable
    {
        void Stop(System.Action onStopCallback);
    }
}
