
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenImageColor : TweenColorTargets<Image>
    {
        public TweenImageColor(Color endValue, Image target) : base(endValue, target)
        {
        }

        public TweenImageColor(Color endValue, params Image[] targets) : base(endValue, targets)
        {
        }

        public TweenImageColor(Color endValue, List<Image> targets) : base(endValue, targets)
        {
        }

        public TweenImageColor(Color startValue, Color endValue, Image target) : base(startValue, endValue, target)
        {
        }

        public TweenImageColor(Color startValue, Color endValue, params Image[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenImageColor(Color startValue, Color endValue, List<Image> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.ImageColor;
        }

        protected override void OnTweenValueUpdate(Color lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.color = lerped;
            }
        }

        protected override void GetStartValue(Image target)
        {
            StartValue = target.color;
        }
    }
}
