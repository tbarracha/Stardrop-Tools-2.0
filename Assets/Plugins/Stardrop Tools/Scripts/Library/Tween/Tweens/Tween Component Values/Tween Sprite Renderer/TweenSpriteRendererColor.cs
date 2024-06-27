
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSpriteRendererColor : TweenColorTargets<SpriteRenderer>
    {
        public TweenSpriteRendererColor(Color endValue, SpriteRenderer target) : base(endValue, target)
        {
        }

        public TweenSpriteRendererColor(Color endValue, params SpriteRenderer[] targets) : base(endValue, targets)
        {
        }

        public TweenSpriteRendererColor(Color endValue, List<SpriteRenderer> targets) : base(endValue, targets)
        {
        }

        public TweenSpriteRendererColor(Color startValue, Color endValue, SpriteRenderer target) : base(startValue, endValue, target)
        {
        }

        public TweenSpriteRendererColor(Color startValue, Color endValue, params SpriteRenderer[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenSpriteRendererColor(Color startValue, Color endValue, List<SpriteRenderer> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.SpriteRendererColor;
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

        protected override void GetStartValue(SpriteRenderer target)
        {
            StartValue = target.color;
        }
    }
}
