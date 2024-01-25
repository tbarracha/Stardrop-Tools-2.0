
public interface ILateInitialize
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