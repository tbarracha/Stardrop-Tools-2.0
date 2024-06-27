
namespace StardropTools
{
    public interface IPlayableCallback : IPlayable
    {
        public void Play(System.Action onPlayCallback);
    }

    public interface IPlayableCallback<T> : IPlayable<T>
    {
        public T Play(System.Action onPlayCallback);
    }
}
