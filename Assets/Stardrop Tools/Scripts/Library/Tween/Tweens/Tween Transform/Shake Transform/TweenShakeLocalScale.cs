
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeLocalScale : TweenShakeVector3
    {
        public Transform target;

        public TweenShakeLocalScale(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakeLocalScale;
        }

        public TweenShakeLocalScale(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.localScale;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakeLocalScale;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.localScale = lerped;
        }
    }
}