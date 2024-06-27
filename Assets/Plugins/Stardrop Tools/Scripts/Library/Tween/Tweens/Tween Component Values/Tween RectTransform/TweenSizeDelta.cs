
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSizeDelta : TweenVector2Targets<RectTransform>
    {
        public TweenSizeDelta(Vector2 startValue, Vector2 endValue, RectTransform target) : base(startValue, endValue, target)
        {
        }

        public TweenSizeDelta(Vector2 startValue, Vector2 endValue, List<RectTransform> targets) : base(startValue, endValue, targets)
        {
        }

        public TweenSizeDelta(Vector2 startValue, Vector2 endValue, params RectTransform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenSizeDelta(Vector2 endValue, RectTransform target) : base(endValue, target)
        {
        }

        public TweenSizeDelta(Vector2 endValue, params RectTransform[] targets) : base(endValue, targets)
        {
        }

        public TweenSizeDelta(Vector2 endValue, List<RectTransform> targets) : base(endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.SizeDelta;
        }

        protected override void OnTweenValueUpdate(Vector2 lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.sizeDelta = lerped;
            }
        }

        protected override void GetStartValue(RectTransform target)
        {
            StartValue = target.sizeDelta;
        }
    }
}
