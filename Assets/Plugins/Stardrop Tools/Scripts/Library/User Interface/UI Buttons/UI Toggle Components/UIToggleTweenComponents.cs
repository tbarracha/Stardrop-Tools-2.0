
using StardropTools.Tween;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleTweenComponents : UIToggleButtonComponent
    {
        [Tooltip("0-false")]
        [SerializeField] TweenComponent[] tweenComponentsOnFalse;

        [Tooltip("1-true")]
        [SerializeField] TweenComponent[] tweenComponentsOnTrue;

        public override void SetToggle(bool value)
        {
            if (value == false)
            {
                foreach (var tweenComponent in tweenComponentsOnTrue)
                    tweenComponent.Stop();

                foreach (var tweenComponent in tweenComponentsOnFalse)
                    tweenComponent.Play();
            }

            else
            {
                foreach (var tweenComponent in tweenComponentsOnFalse)
                    tweenComponent.Stop();

                foreach (var tweenComponent in tweenComponentsOnTrue)
                    tweenComponent.Play();
            }
        }
    }
}