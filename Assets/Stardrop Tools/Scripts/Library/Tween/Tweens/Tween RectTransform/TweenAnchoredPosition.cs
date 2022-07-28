
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenAnchoredPosition : TweenVector2
    {
        public RectTransform target;

        public TweenAnchoredPosition(RectTransform target, Vector2 start, Vector2 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.AnchoredPosition;
        }

        public TweenAnchoredPosition(RectTransform target, Vector2 end)
        {
            this.target = target;
            start = target.anchoredPosition;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.AnchoredPosition;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.anchoredPosition = lerped;
        }
    }
}