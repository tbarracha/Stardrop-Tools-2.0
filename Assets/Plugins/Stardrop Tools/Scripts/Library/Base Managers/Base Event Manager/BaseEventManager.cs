
namespace StardropTools
{
    public abstract class BaseEventManager
    {
        public static class InitializationEvents
        {
            public static readonly EventCallback OnInitialized = new EventCallback();
            public static readonly EventCallback OnPoolsInitialized = new EventCallback();
        }

        public static class SystemEvents
        {
            public static readonly EventCallback OnDataSaved = new EventCallback();
            public static readonly EventCallback OnDataLoaded = new EventCallback();
            public static readonly EventCallback OnQuit = new EventCallback();
        }
    }
}
