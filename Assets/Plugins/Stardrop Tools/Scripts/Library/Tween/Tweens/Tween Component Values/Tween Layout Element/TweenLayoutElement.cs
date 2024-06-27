using System.Collections.Generic;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenLayoutElement : TweenFloatTargets<LayoutElement>
    {
        protected TweenLayoutElementTarget layoutTarget;

        public TweenLayoutElement(float endValue, TweenLayoutElementTarget layoutTarget, LayoutElement target) : base(endValue, target)
        {
            this.layoutTarget = layoutTarget;
        }

        public TweenLayoutElement(float endValue, TweenLayoutElementTarget layoutTarget, params LayoutElement[] targets) : base(endValue, targets)
        {
            this.layoutTarget = layoutTarget;
        }

        public TweenLayoutElement(float endValue, TweenLayoutElementTarget layoutTarget, List<LayoutElement> targets) : base(endValue, targets)
        {
            this.layoutTarget = layoutTarget;
        }

        public TweenLayoutElement(float startValue, float endValue, TweenLayoutElementTarget layoutTarget, LayoutElement target) : base(startValue, endValue, target)
        {
            this.layoutTarget = layoutTarget;
        }

        public TweenLayoutElement(float startValue, float endValue, TweenLayoutElementTarget layoutTarget, params LayoutElement[] targets) : base(startValue, endValue, targets)
        {
            this.layoutTarget = layoutTarget;
        }

        public TweenLayoutElement(float startValue, float endValue, TweenLayoutElementTarget layoutTarget, List<LayoutElement> targets) : base(startValue, endValue, targets)
        {
            this.layoutTarget = layoutTarget;
        }

        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.LayoutElement;
        }

        protected override void OnTweenValueUpdate(float lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

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

        protected override void GetStartValue(LayoutElement target)
        {
            if (layoutTarget == TweenLayoutElementTarget.MinWidth)
                StartValue = target.minWidth;

            else if (layoutTarget == TweenLayoutElementTarget.MinHeight)
                StartValue = target.minHeight;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredWidth)
                StartValue = target.preferredWidth;

            else if (layoutTarget == TweenLayoutElementTarget.PreferredHeight)
                StartValue = target.preferredHeight;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleWidth)
                StartValue = target.flexibleWidth;

            else if (layoutTarget == TweenLayoutElementTarget.FlexibleHeight)
                StartValue = target.flexibleHeight;
        }
    }
}
