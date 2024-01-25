
namespace StardropTools
{
    public interface ITriggableParams : ITriggable
    {
        public void TriggerBehaviour<T>(params T[] parameters);
    }
}
