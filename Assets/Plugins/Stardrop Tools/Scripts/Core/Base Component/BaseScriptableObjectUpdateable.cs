
namespace StardropTools
{
    //[CreateAssetMenu(fileName = "Base Updateable Scriptable Object", menuName = "Stardrop / Scriptables / New Base Updateable Scriptable List")]
    public abstract class BaseScriptableObjectUpdateable : BaseScriptableObject, IUpdateable
    {
        public bool IsUpdating { get; protected set; }
        
        public virtual void StartUpdate()
        {
            if (IsUpdating == true)
                return;

            LoopManager.AddToUpdate(this);
            IsUpdating = true;
        }

        public virtual void StopUpdate()
        {
            if (IsUpdating == false)
                return;

            LoopManager.RemoveFromUpdate(this);
            IsUpdating = false;
        }

        public virtual void HandleUpdate() { }
    }
}