
public interface IInitialize
{
    public void Initialize();

    /*
    public bool IsInitialized { get; protected set; } = false;

    public virtual void Initialize()
    {
        if (IsInitialized == true)
            return;

        IsInitialized = true;
    }
    */
}