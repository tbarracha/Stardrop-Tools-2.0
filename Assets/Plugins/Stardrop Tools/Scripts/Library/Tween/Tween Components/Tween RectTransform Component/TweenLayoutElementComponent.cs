
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenLayoutElementComponent : TweenFloatComponent
    {
        [Header("Target Layout Element")]
        public LayoutElement target;
        public TweenLayoutElementTarget layoutTarget;

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenLayoutElement(target, layoutTarget, startValue, endValue);
            else
                tween = new TweenLayoutElement(target, layoutTarget, endValue);

            SetTweenEssentials();
            tween.SetID(this).Play();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Value")]
        private void GetStart()
        {
            if (layoutTarget == TweenLayoutElementTarget.MinWidth)
                startValue = target.minHeight;
            
            else if (layoutTarget == TweenLayoutElementTarget.MinHeight)
                startValue = target.minHeight;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredWidth)
                startValue = target.preferredWidth;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredHeight)
                startValue = target.preferredHeight;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleWidth)
                startValue = target.flexibleWidth;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleHeight)
                startValue = target.flexibleHeight;
        }

        [NaughtyAttributes.Button("Get Layout Element")]
        void GetSelfLayoutElement()
        {
            target = GetComponent<LayoutElement>();
        }
    }
}