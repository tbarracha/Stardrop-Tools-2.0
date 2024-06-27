
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenEulerRotation : TweenVector3Targets<Transform>
    {
        public TweenEulerRotation(Vector3 endValue, Transform target) : base(endValue, target)
        {
        }

        public TweenEulerRotation(Vector3 endValue, params Transform[] targets) : base(endValue, targets)
        {
        }

        public TweenEulerRotation(Vector3 endValue, List<Transform> targets) : base(endValue, targets)
        {
        }

        public TweenEulerRotation(Vector3 startValue, Vector3 endValue, Transform target) : base(startValue, endValue, target)
        {
        }

        public TweenEulerRotation(Vector3 startValue, Vector3 endValue, params Transform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenEulerRotation(Vector3 startValue, Vector3 endValue, List<Transform> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.EulerRotation;
        }

        protected override void OnTweenValueUpdate(Vector3 lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.eulerAngles = lerped;
            }
        }

        protected override void GetStartValue(Transform target)
        {
            StartValue = target.eulerAngles;
        }
    }

    public class TweenLocalEulerRotation : TweenVector3Targets<Transform>
    {
        public TweenLocalEulerRotation(Vector3 endValue, Transform target) : base(endValue, target)
        {
        }

        public TweenLocalEulerRotation(Vector3 endValue, params Transform[] targets) : base(endValue, targets)
        {
        }

        public TweenLocalEulerRotation(Vector3 endValue, List<Transform> targets) : base(endValue, targets)
        {
        }

        public TweenLocalEulerRotation(Vector3 startValue, Vector3 endValue, Transform target) : base(startValue, endValue, target)
        {
        }

        public TweenLocalEulerRotation(Vector3 startValue, Vector3 endValue, params Transform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenLocalEulerRotation(Vector3 startValue, Vector3 endValue, List<Transform> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.LocalEulerRotation;
        }

        protected override void OnTweenValueUpdate(Vector3 lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.localEulerAngles = lerped;
            }
        }

        protected override void GetStartValue(Transform target)
        {
            StartValue = target.localEulerAngles;
        }
    }
}
