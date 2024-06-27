
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenAnchoredPosition : TweenVector2Targets<RectTransform>
    {
        public TweenAnchoredPosition(Vector2 startValue, Vector2 endValue, RectTransform target) : base(startValue, endValue, target)
        {
        }

        public TweenAnchoredPosition(Vector2 startValue, Vector2 endValue, List<RectTransform> targets) : base(startValue, endValue, targets)
        {
        }

        public TweenAnchoredPosition(Vector2 startValue, Vector2 endValue, params RectTransform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenAnchoredPosition(Vector2 endValue, RectTransform target) : base(endValue, target)
        {
        }

        public TweenAnchoredPosition(Vector2 endValue, params RectTransform[] targets) : base(endValue, targets)
        {
        }

        public TweenAnchoredPosition(Vector2 endValue, List<RectTransform> targets) : base(endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.AnchoredPosition;
        }

        protected override void OnTweenValueUpdate(Vector2 lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.anchoredPosition = lerped;
            }
        }

        protected override void GetStartValue(RectTransform target)
        {
            StartValue = target.anchoredPosition;
        }
    }
}
