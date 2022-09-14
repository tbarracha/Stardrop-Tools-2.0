
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSpriteRendererColor : TweenColor
    {
        public SpriteRenderer renderer;

        protected override void SetEssentials()
        {
            //tweenID = image.GetInstanceID();
            tweenType = TweenType.SpriteRendererColor;
        }

        public TweenSpriteRendererColor(SpriteRenderer renderer, Color start, Color end)
        {
            this.renderer = renderer;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenSpriteRendererColor(SpriteRenderer renderer, Color end)
        {
            this.renderer = renderer;
            start = renderer.color;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            renderer.color = lerped;
        }
    }
}