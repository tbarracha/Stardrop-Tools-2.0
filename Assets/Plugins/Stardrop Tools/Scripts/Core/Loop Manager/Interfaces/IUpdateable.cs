
namespace StardropTools
{
    public interface IUpdateable
    {
        /// <summary>
        /// Adds object to the Update list in the LoopManager
        /// </summary>
        public void StartUpdate();

        /// <summary>
        /// Removes object to the Update list in the LoopManager
        /// </summary>
        public void StopUpdate();

        public void HandleUpdate();


        /*
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
        */
    }
}