
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLineColorOpacity : TweenColorOpacityTargets<LineRenderer>
    {
        public TweenLineColorOpacity(Color color, float startValue, float endValue, LineRenderer target) : base(color, startValue, endValue, target)
        {
        }

        public TweenLineColorOpacity(Color color, float startValue, float endValue, params LineRenderer[] targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenLineColorOpacity(Color color, float startValue, float endValue, List<LineRenderer> targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenLineColorOpacity(Color color, float endValue, LineRenderer target) : base(color, endValue, target)
        {
        }

        public TweenLineColorOpacity(Color color, float endValue, params LineRenderer[] targets) : base(color, endValue, targets)
        {
        }

        public TweenLineColorOpacity(Color color, float endValue, List<LineRenderer> targets) : base(color, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.LineRendererOpacity;
        }

        protected override void OnTweenValueUpdate(float lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.startColor = LerpedColor;
                item.endColor = LerpedColor;
            }
        }

        protected override void GetStartValue(LineRenderer target)
        {
            StartValue = target.startColor.a;
        }
    }
}
