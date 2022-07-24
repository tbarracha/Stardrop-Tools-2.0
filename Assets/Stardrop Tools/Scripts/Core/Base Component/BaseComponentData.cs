

namespace StardropTools
{
    /// <summary>
    /// BaseComponent Initialization focused data
    /// </summary>
    [System.Serializable]
    public struct BaseComponentData
    {
        [UnityEngine.SerializeField] EBaseInitialization initializationAt;
        [UnityEngine.SerializeField] EBaseInitialization lateInitializationAt;
        public bool stopUpdateOnDisable;

        public EBaseInitialization InitializationAt { get => initializationAt; }
        public EBaseInitialization LateInitializationAt { get => lateInitializationAt; }
    }
}

