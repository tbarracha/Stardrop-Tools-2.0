
namespace StardropTools
{
    public interface IPlayableWithAction<T> : IPlayable<T>
    {
        public T Play(System.Action onPlayCallback);
        public void Stop(System.Action onStopCallback);
    }
}
