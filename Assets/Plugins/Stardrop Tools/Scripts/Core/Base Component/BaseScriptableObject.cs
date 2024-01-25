
using UnityEngine;

namespace StardropTools
{
    //[CreateAssetMenu(fileName = "Base Scriptable Object", menuName = "Stardrop / Scriptables / New Base Scriptable List")]
    public abstract class BaseScriptableObject : ScriptableObject, IInitialize, ILateInitialize
    {
        public bool IsInitialized { get; protected set; } = false;
        public bool IsLateInitialized { get; protected set; } = false;

        public virtual void Initialize()
        {
            if (IsInitialized == true)
                return;

            IsInitialized = true;
        }

        public void LateInitialize()
        {
            if (IsLateInitialized == true)
                return;

            IsLateInitialized = true;
        }

        protected void print(object message)
        {
            Debug.Log(message);
        }
    }
}