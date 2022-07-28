
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakePosition : TweenShakeVector3
    {
        public Transform target;

        public TweenShakePosition(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.Position;
        }

        public TweenShakePosition(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.position;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.Position;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.position = lerped;
        }
    }


    // Local Position
    public class TweenShakeLocalPosition : TweenShakeVector3
    {
        public Transform target;

        public TweenShakeLocalPosition(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenType = TweenType.Position;
        }

        public TweenShakeLocalPosition(Transform target, Vector3 end)
        {
            this.target = target;

            start = target.localPosition;
            this.end = end;

            tweenType = TweenType.LocalPosition;
        }


        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.localPosition = lerped;
        }
    }
}