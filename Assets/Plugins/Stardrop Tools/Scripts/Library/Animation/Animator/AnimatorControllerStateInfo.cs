
using UnityEngine;

namespace StardropTools.Animation
{
    [System.Serializable]
    public struct AnimatorControllerStateInfo
    {
        [Header("State Info")]
        [SerializeField] private string stateName;
        [SerializeField] private int stateHash;
        [SerializeField] int layerIndex;
        [SerializeField] float lengthInSeconds;

        [Header("Play Type")]
        [SerializeField] AnimatorControllerStateChangeType stateChangeType;
        [Range(0, .5f)]
        [SerializeField] float crossfade;
        [SerializeField] string trigger;
        [SerializeField] float triggerResetTime;

        public static readonly AnimatorControllerStateInfo Empty = new AnimatorControllerStateInfo(string.Empty, 0, 0, 0, AnimatorControllerStateChangeType.CrossFade, 0.15f, string.Empty, 0.1f);

        public AnimatorControllerStateInfo(string stateName, int stateHash, int layerIndex, float lengthInSeconds, AnimatorControllerStateChangeType stateChangeType, float crossfade, string trigger, float triggerResetTime)
        {
            this.stateName = stateName;
            this.stateHash = stateHash;
            this.layerIndex = layerIndex;
            this.lengthInSeconds = lengthInSeconds;
            this.stateChangeType = stateChangeType;
            this.crossfade = crossfade;
            this.trigger = trigger;
            this.triggerResetTime = triggerResetTime;
        }

        public string StateName { get => stateName; set => stateName = value; }
        public int StateHash { get => stateHash; set => stateHash = value; }
        public int LayerIndex { get => layerIndex; set => layerIndex = value; }
        public float LengthInSeconds { get => lengthInSeconds; set => lengthInSeconds = value; }
        public AnimatorControllerStateChangeType StateChangeType { get => stateChangeType; set => stateChangeType = value; }
        public float Crossfade { get => crossfade; set => crossfade = value; }
        public string Trigger { get => trigger; set => trigger = value; }
        public float TriggtriggerResetTimeer { get => triggerResetTime; set => triggerResetTime = value; }

        public override bool Equals(object obj)
        {
            return obj is AnimatorControllerStateInfo info &&
                   stateName == info.stateName &&
                   stateHash == info.stateHash;
        }
    }
}
