
namespace StardropTools
{
    public interface IPlayable
    {
        public void Play();
    }

    public interface IPlayable<T>
    {
        public T Play();
    }
}
