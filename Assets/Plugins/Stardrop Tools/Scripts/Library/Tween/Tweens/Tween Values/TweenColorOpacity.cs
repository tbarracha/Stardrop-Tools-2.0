
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenColorOpacity : TweenFloat
    {
        protected Color color;
        public Color LerpedColor => color;

        public TweenColorOpacity(Color color, float startValue, float endValue) : base(startValue, endValue)
        {
            this.color = color;
        }

        public TweenColorOpacity(Color color, float endValue) : base(endValue)
        {
            this.color = color;
        }

        public TweenColorOpacity() { }

        protected override void SetEssentials()
        {
            TweenType = TweenType.ColorOpacity;
        }

        protected override void TweenUpdate(float percent)
        {
            color.a = Lerped;
            base.TweenUpdate(percent);
        }
    }
}
