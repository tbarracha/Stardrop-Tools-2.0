
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLineColor : TweenColorTargets<LineRenderer>
    {
        public TweenLineColor(Color endValue, LineRenderer target) : base(endValue, target)
        {
        }

        public TweenLineColor(Color endValue, params LineRenderer[] targets) : base(endValue, targets)
        {
        }

        public TweenLineColor(Color endValue, List<LineRenderer> targets) : base(endValue, targets)
        {
        }

        public TweenLineColor(Color startValue, Color endValue, LineRenderer target) : base(startValue, endValue, target)
        {
        }

        public TweenLineColor(Color startValue, Color endValue, params LineRenderer[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenLineColor(Color startValue, Color endValue, List<LineRenderer> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.LineRendererColor;
        }

        protected override void OnTweenValueUpdate(Color lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.startColor = lerped;
                item.endColor = lerped;
            }
        }

        protected override void GetStartValue(LineRenderer target)
        {
            StartValue = target.startColor;
        }
    }
}
