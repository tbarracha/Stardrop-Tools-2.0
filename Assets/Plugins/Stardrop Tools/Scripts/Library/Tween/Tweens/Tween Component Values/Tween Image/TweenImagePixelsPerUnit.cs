
using System.Collections.Generic;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenImagePixelsPerUnit : TweenFloatTargets<Image>
    {
        public TweenImagePixelsPerUnit(float endValue, Image target) : base(endValue, target)
        {
        }

        public TweenImagePixelsPerUnit(float endValue, params Image[] targets) : base(endValue, targets)
        {
        }

        public TweenImagePixelsPerUnit(float endValue, List<Image> targets) : base(endValue, targets)
        {
        }

        public TweenImagePixelsPerUnit(float startValue, float endValue, Image target) : base(startValue, endValue, target)
        {
        }

        public TweenImagePixelsPerUnit(float startValue, float endValue, params Image[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenImagePixelsPerUnit(float startValue, float endValue, List<Image> targets) : base(startValue, endValue, targets)
        {
        }

        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.ImagePixelsPerUnitMultiplier;
        }

        protected override void OnTweenValueUpdate(float lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.pixelsPerUnitMultiplier = lerped;
            }
        }

        protected override void GetStartValue(Image target)
        {
            StartValue = target.pixelsPerUnitMultiplier;
        }
    }
}
