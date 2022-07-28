
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRectSize : TweenVector2
    {
        public RectTransform target;
        Rect rect;

        public TweenRectSize(RectTransform target, Vector2 start, Vector2 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.SizeDelta;
        }

        public TweenRectSize(RectTransform target, Vector2 end)
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