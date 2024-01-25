
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenLayoutElement : TweenFloat
    {
        public LayoutElement target;
        public TweenLayoutElementTarget layoutTarget;

        protected override void SetEssentials()
        {
            //tweenID = target.GetHashCode();
            tweenType = TweenType.RectSize;
        }

        public TweenLayoutElement(LayoutElement target, TweenLayoutElementTarget layoutTarget, float start, float end)
        {
            this.layoutTarget   = layoutTarget;
            this.target         = target;
            this.start          = start;
            this.end            = end;

            SetEssentials();
        }

        public TweenLayoutElement(LayoutElement target, TweenLayoutElementTarget layoutTarget, float end)
        {
            this.target         = target;
            this.layoutTarget   = layoutTarget;

            if (layoutTarget == TweenLayoutElementTarget.MinWidth)
                start = target.minWidth;

            else if (layoutTarget == TweenLayoutElementTarget.MinHeight)
                start = target.minHeight;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredWidth)
                start = target.preferredWidth;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredHeight)
                start = target.preferredHeight;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleWidth)
                start = target.flexibleWidth;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleHeight)
                start = target.flexibleHeight;

            this.end = end;
            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (target == null)
                ChangeState(TweenState.Canceled);

            if (layoutTarget == TweenLayoutElementTarget.MinWidth)
                target.minWidth = lerped;

            else if (layoutTarget == TweenLayoutElementTarget.MinHeight)
                target.minHeight = lerped;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredWidth)
                target.preferredWidth = lerped;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredHeight)
                target.preferredHeight = lerped;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleWidth)
                target.flexibleWidth = lerped;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleHeight)
                target.flexibleHeight = lerped;
        }
    }
}