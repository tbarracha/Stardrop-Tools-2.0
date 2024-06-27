
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenImageColorOpacity : TweenColorOpacityTargets<Image>
    {
        public TweenImageColorOpacity(Color color, float startValue, float endValue, Image target) : base(color, startValue, endValue, target)
        {
        }

        public TweenImageColorOpacity(Color color, float startValue, float endValue, params Image[] targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenImageColorOpacity(Color color, float startValue, float endValue, List<Image> targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenImageColorOpacity(Color color, float endValue, Image target) : base(color, endValue, target)
        {
        }

        public TweenImageColorOpacity(Color color, float endValue, params Image[] targets) : base(color, endValue, targets)
        {
        }

        public TweenImageColorOpacity(Color color, float endValue, List<Image> targets) : base(color, endValue, targets)
        {
        }



        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.ImageOpacity;
        }

        protected override void OnTweenValueUpdate(float lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.color = LerpedColor;
            }
        }

        protected override void GetStartValue(Image target)
        {
            StartValue = target.color.a;
        }
    }
}
