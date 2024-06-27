
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSpriteRendererColorOpacity : TweenColorOpacityTargets<SpriteRenderer>
    {
        public TweenSpriteRendererColorOpacity(Color color, float startValue, float endValue, SpriteRenderer target) : base(color, startValue, endValue, target)
        {
        }

        public TweenSpriteRendererColorOpacity(Color color, float startValue, float endValue, params SpriteRenderer[] targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenSpriteRendererColorOpacity(Color color, float startValue, float endValue, List<SpriteRenderer> targets) : base(color, startValue, endValue, targets)
        {
        }

        public TweenSpriteRendererColorOpacity(Color color, float endValue, SpriteRenderer target) : base(color, endValue, target)
        {
        }

        public TweenSpriteRendererColorOpacity(Color color, float endValue, params SpriteRenderer[] targets) : base(color, endValue, targets)
        {
        }

        public TweenSpriteRendererColorOpacity(Color color, float endValue, List<SpriteRenderer> targets) : base(color, endValue, targets)
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

        protected override void GetStartValue(SpriteRenderer target)
        {
            StartValue = target.color.a;
        }
    }
}
