
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRotation : TweenQuaternion
    {
        public Transform target;

        public TweenRotation(Transform target, Quaternion start, Quaternion end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.Rotation;
        }

        public TweenRotation(Transform target, Quaternion end)
        {
            this.target = target;
            start = target.rotation;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.Rotation;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.rotation = lerped;
        }
    }


    // Local Rotation
    public class TweenLocalRotation : TweenQuaternion
    {
        public Transform target;

        public TweenLocalRotation(Transform target, Quaternion start, Quaternion end)
        {
            this.target = target;
            this.start = start;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.LocalRotation;
        }

        public TweenLocalRotation(Transform target, Quaternion end)
        {
            this.target = target;
            start = target.localRotation;
            this.end = end;

            tweenID = target.GetInstanceID();
            tweenType = TweenType.LocalRotation;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            target.localRotation = lerped;
        }
    }
}