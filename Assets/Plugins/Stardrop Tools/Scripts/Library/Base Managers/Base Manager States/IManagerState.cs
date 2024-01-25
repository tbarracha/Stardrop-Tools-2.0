
namespace StardropTools
{
    public interface IManagerState
    {
        public int    StateID { get; set; }
        public string StateName { get; }

        public void   EnterState(IManagerState previousState);
        public void   ExitState();
    }
}
