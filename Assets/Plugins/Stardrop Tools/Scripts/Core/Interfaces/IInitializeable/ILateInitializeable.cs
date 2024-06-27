namespace StardropTools
{
    public interface ILateInitializeable
    {
        public void LateInitialize();

        /*
        public bool IsLateInitialized { get; protected set; } = false;

        public void LateInitialize()
        {
            if (IsLateInitialized == true)
                return;

            IsLateInitialized = true;
        }
        */
    }

    public interface ILateInitializeable<T>
    {
        public void LateInitialize(T item);

        /*
        public bool IsLateInitialized { get; protected set; } = false;

        public void LateInitialize(T item)
        {
            if (IsLateInitialized == true)
                return;

            IsLateInitialized = true;
        }
        */
    }
}