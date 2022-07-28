
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSizeDelta : TweenVector2
    {
        public RectTransform target;

        public TweenSizeDelta(RectTransform target, Vector2 start, Vector2 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.SizeDelta;
        }

        public TweenSizeDelta(RectTransform target, Vector2 end)
        {
            this.target = target;
            start = target.sizeDelta;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.SizeDelta;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.sizeDelta = lerped;
        }
    }
}