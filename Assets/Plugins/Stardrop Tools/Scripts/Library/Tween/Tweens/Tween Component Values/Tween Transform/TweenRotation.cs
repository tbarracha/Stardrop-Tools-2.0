
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRotation : TweenQuaternionTargets<Transform>
    {
        public TweenRotation(Quaternion endValue, Transform target) : base(endValue, target)
        {
        }

        public TweenRotation(Quaternion endValue, params Transform[] targets) : base(endValue, targets)
        {
        }

        public TweenRotation(Quaternion endValue, List<Transform> targets) : base(endValue, targets)
        {
        }

        public TweenRotation(Quaternion startValue, Quaternion endValue, Transform target) : base(startValue, endValue, target)
        {
        }

        public TweenRotation(Quaternion startValue, Quaternion endValue, params Transform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenRotation(Quaternion startValue, Quaternion endValue, List<Transform> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.Rotation;
        }

        protected override void OnTweenValueUpdate(Quaternion lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.rotation = lerped;
            }
        }

        protected override void GetStartValue(Transform target)
        {
            StartValue = target.rotation;
        }
    }

    public class TweenLocalRotation : TweenQuaternionTargets<Transform>
    {
        public TweenLocalRotation(Quaternion endValue, Transform target) : base(endValue, target)
        {
        }

        public TweenLocalRotation(Quaternion endValue, params Transform[] targets) : base(endValue, targets)
        {
        }

        public TweenLocalRotation(Quaternion endValue, List<Transform> targets) : base(endValue, targets)
        {
        }

        public TweenLocalRotation(Quaternion startValue, Quaternion endValue, Transform target) : base(startValue, endValue, target)
        {
        }

        public TweenLocalRotation(Quaternion startValue, Quaternion endValue, params Transform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenLocalRotation(Quaternion startValue, Quaternion endValue, List<Transform> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.LocalRotation;
        }

        protected override void OnTweenValueUpdate(Quaternion lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.localRotation = lerped;
            }
        }

        protected override void GetStartValue(Transform target)
        {
            StartValue = target.localRotation;
        }
    }
}
