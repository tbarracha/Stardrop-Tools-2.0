
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenColorOpacityTargets<TweenedTargetType> : TweenTargets<float, TweenedTargetType>
    {
        protected Color color;
        public Color LerpedColor => color;

        protected TweenColorOpacityTargets(Color color, float startValue, float endValue, TweenedTargetType target) : base(startValue, endValue, target)
        {
            this.color = color;
        }

        protected TweenColorOpacityTargets(Color color, float startValue, float endValue, params TweenedTargetType[] targets) : base(startValue, endValue, targets)
        {
            this.color = color;
        }

        protected TweenColorOpacityTargets(Color color, float startValue, float endValue, List<TweenedTargetType> targets) : base(startValue, endValue, targets)
        {
            this.color = color;
        }


        protected TweenColorOpacityTargets(Color color, float endValue, TweenedTargetType target) : base(endValue, target)
        {
            this.color = color;
            StartValue = color.a;
        }

        protected TweenColorOpacityTargets(Color color, float endValue, params TweenedTargetType[] targets) : base(endValue, targets)
        {
            this.color = color;
            StartValue = color.a;
        }

        protected TweenColorOpacityTargets(Color color, float endValue, List<TweenedTargetType> targets) : base(endValue, targets)
        {
            this.color = color;
            StartValue = color.a;
        }



        protected override void SetEssentials()
        {
            TweenType = TweenType.ColorOpacity;
            color.a = StartValue;
        }

        protected override void TweenUpdate(float percent)
        {
            Lerped = Mathf.LerpUnclamped(StartValue, EndValue, Ease(percent));
            color.a = Lerped;

            base.TweenUpdate(percent);
        }
    }
}
