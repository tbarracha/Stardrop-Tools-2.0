
namespace StardropTools
{
    public interface IPlayable<T>
    {
        public T Play();
        public void Stop();
    }
}
