
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLocalScale : TweenVector3
    {
        public Transform target;

        public TweenLocalScale(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.LocalScale;
        }

        public TweenLocalScale(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.localScale;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.LocalScale;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.localScale = lerped;
        }
    }
}