
using UnityEngine;

namespace StardropTools
{
    /// <summary>
    /// Used in conjunction with an Animator Component. Detects animation events based on event ID
    /// </summary>
    public class AnimationEventDetector : MonoBehaviour
    {
        [SerializeField] bool debug;

        public readonly BaseEvent<int> OnAnimEvent = new BaseEvent<int>();

        public void AnimationEvent(int eventID)
        {
            if (debug)
                Debug.LogFormat("Anim event: {0}, detected!", eventID);

            OnAnimEvent?.Invoke(eventID);
        }
    }
}
