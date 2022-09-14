
using UnityEngine;

namespace StardropTools
{
    public abstract class ScriptableValue : ScriptableObject
    {
#if UNITY_EDITOR
        [TextArea][SerializeField] protected string description;
#endif

        public abstract void Default();
    }
}