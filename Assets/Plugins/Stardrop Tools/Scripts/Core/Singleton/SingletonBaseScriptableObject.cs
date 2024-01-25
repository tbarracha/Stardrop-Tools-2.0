using UnityEngine;

namespace StardropTools
{
    //[CreateAssetMenu(fileName = "Base Scriptable Object", menuName = "Stardrop / Scriptables / New Base Scriptable List")]
    public abstract class SingletonBaseScriptableObject<T> : BaseScriptableObject where T : SingletonBaseScriptableObject<T>
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
                    //T[] results = Resources.LoadAll<T>(Instance.searchPath);
                    T[] results = Resources.FindObjectsOfTypeAll<T>();

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
