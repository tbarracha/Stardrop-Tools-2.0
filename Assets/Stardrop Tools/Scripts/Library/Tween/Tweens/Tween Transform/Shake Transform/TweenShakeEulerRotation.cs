
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeEulerRotation : TweenShakeVector3
    {
        public Transform target;

        public TweenShakeEulerRotation(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakeEulerRotation;
        }

        public TweenShakeEulerRotation(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.eulerAngles;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.ShakeEulerRotation;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.eulerAngles = lerped;
        }
    }


    // Local Rotation
    public class TweenShakeLocalEulerRotation : TweenShakeVector3
    {
        public Transform target;

        public TweenShakeLocalEulerRotation(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenType = TweenType.ShakeLocalEulerRotation;
        }

        public TweenShakeLocalEulerRotation(Transform target, Vector3 end)
        {
            this.target = target;

            start = target.localEulerAngles;
            this.end = end;

            tweenType = TweenType.ShakeLocalEulerRotation;
        }


        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.localEulerAngles = lerped;
        }
    }
}