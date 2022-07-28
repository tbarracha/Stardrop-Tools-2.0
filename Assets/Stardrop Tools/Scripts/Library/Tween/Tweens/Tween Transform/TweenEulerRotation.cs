
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenEulerRotation : TweenVector3
    {
        public Transform target;

        public TweenEulerRotation(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.EulerRotation;
        }

        public TweenEulerRotation(Transform target, Vector3 end)
        {
            this.target = target;
            start = target.eulerAngles;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.EulerRotation;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.eulerAngles = lerped;
        }
    }


    // Local Rotation
    public class TweenLocalEulerRotation : TweenVector3
    {
        public Transform target;

        public TweenLocalEulerRotation(Transform target, Vector3 start, Vector3 end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenType = TweenType.LocalEulerRotation;
        }

        public TweenLocalEulerRotation(Transform target, Vector3 end)
        {
            this.target = target;

            start = target.localEulerAngles;
            this.end = end;

            tweenType = TweenType.LocalEulerRotation;
        }


        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.localEulerAngles = lerped;
        }
    }
}