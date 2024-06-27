namespace StardropTools
{
    public interface IInitializeable
    {
        public void Initialize();

        /*
        public bool IsInitialized { get; protected set; } = false;

        public virtual void Initialize()
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
        }
        */
    }

    public interface IInitializeable<T>
    {
        public void Initialize(T item);

        /*
        public bool IsInitialized { get; protected set; } = false;

        public virtual void Initialize(T item)
        {
            if (IsInitialized)
                return;

            IsInitialized = true;
        }
        */
    }
}