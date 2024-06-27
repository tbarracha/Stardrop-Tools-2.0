
using UnityEngine;

namespace StardropTools
{
    public class AnimationClipManager : MonoBehaviour
    {
        [SerializeField] UnityEngine.Animation animationComponent;
        [SerializeField] int currentClipID;
        [SerializeField] AnimationClip[] clips;

        public void PlayAnimation(int clipID, bool ignoreSameID = false)
        {
            if (ignoreSameID == false && currentClipID == clipID)
                return;

            currentClipID = clipID;
            animationComponent.Stop();
            animationComponent.clip = clips[currentClipID];
            animationComponent.Play();
        }
    }
}