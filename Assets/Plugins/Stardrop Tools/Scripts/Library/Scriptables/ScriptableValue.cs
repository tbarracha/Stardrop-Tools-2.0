
using UnityEngine;

namespace StardropTools.Scriptables
{
    // [CreateAssetMenu(fileName = "Scriptable Value", menuName = defaultMenuName + "Scriptable Value")]
    public abstract class ScriptableValue : ScriptableObject
    {
        /// <summary>
        /// CreateAssetMenu, menuName variable first part. Just continue with ( + "Class Name")
        /// </summary>
        protected const string defaultMenuName = "Stardrop / Scriptables / ";
        protected const string debugSave = "SAVE";
        protected const string debugLoad = "LOAD";

        public int ValueID;

        public abstract void Save();
        public abstract void Save(string key);
        public abstract void Load();
        public abstract void Load(string key);
        public abstract void DeleteKey();
        public abstract void ResetValue();

        public virtual void ResetValue(bool save)
        {
            ResetValue();

            if (save)
                Save();
        }

        protected bool IsValidKey(string key, string saveOrLoad)
        {
            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError($"Failed to {saveOrLoad} scriptable value: {name}. NO KEY");
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
