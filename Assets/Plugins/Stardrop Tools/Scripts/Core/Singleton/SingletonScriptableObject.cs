using UnityEngine;

namespace StardropTools
{
    public abstract class SingletonScriptableObject<T> : BaseScriptableObject where T : SingletonScriptableObject<T>
    {
        [SerializeField]
        private string searchPath = "Scriptable Singletons";

        private static T instance = null;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    T[] results = Resources.LoadAll<T>(Instance.searchPath);

                    if (results.Length == 0)
                    {
                        Debug.LogError($"SingletonScriptableObject not found of {typeof(T)} in '{Instance.searchPath}' folder");
                        return null;
                    }

                    if (results.Length > 1)
                    {
                        Debug.LogError($"SingletonScriptableObject of {typeof(T)} found more than one in '{Instance.searchPath}' folder. There should only be one!");
                        return null;
                    }

                    instance = results[0];
                    instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
                }

                return instance;
            }
        }
    }
}
