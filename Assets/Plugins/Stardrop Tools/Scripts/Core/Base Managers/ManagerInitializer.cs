using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Class that finds all child objects with the IManager interface and calls its methods in the following order : InitializeManager(), LateInitializeManager()
    /// <para> Note : NOT a singleton </para>
    /// </summary>
    public class ManagerInitializer : BaseComponent
    {
        [SerializeField] protected Transform parentManagers;
        protected IManager[] managers;

        public override void Initialize()
        {
            base.Initialize();

            GetManagers();
            InitializeManagers();
        }

        protected virtual void GetManagers()
            => managers = Utilities.GetComponentArrayInChildren<IManager>(parentManagers);

        protected virtual void InitializeManagers()
        {
            if (managers.Exists() == false)
                GetManagers();

            Utilities.InitializeManagers(managers);
            Utilities.LateInitializeManagers(managers);
            OnManagersInitialized();
        }


        protected virtual void OnManagersInitialized()
        {
            Debug.Log("Managers Initialized!");
        }
        

        [NaughtyAttributes.Button("Make this as Parent")]
        protected void MakeThisObjectParent()
        {
            parentManagers = transform;
        }
    }
}