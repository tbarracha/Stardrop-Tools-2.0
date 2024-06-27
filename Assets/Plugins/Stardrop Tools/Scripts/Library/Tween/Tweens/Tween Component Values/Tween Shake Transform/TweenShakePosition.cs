
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakePosition : TweenShakeVector3Targets<Transform>
    {
        public TweenShakePosition(Vector3 endValue, Vector3 intensity, Transform target) : base(endValue, intensity, target)
        {
        }

        public TweenShakePosition(Vector3 endValue, Vector3 intensity, params Transform[] targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakePosition(Vector3 endValue, Vector3 intensity, List<Transform> targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakePosition(Vector3 startValue, Vector3 endValue, Vector3 intensity, Transform target) : base(startValue, endValue, intensity, target)
        {
        }

        public TweenShakePosition(Vector3 startValue, Vector3 endValue, Vector3 intensity, params Transform[] targets) : base(startValue, endValue, intensity, targets)
        {
        }

        public TweenShakePosition(Vector3 startValue, Vector3 endValue, Vector3 intensity, List<Transform> targets) : base(startValue, endValue, intensity, targets)
        {
        }


        // float intensity
        public TweenShakePosition(Vector3 endValue, float intensity, Transform target) : base(endValue, intensity, target)
        {
        }

        public TweenShakePosition(Vector3 endValue, float intensity, params Transform[] targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakePosition(Vector3 endValue, float intensity, List<Transform> targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakePosition(Vector3 startValue, Vector3 endValue, float intensity, Transform target) : base(startValue, endValue, intensity, target)
        {
        }

        public TweenShakePosition(Vector3 startValue, Vector3 endValue, float intensity, params Transform[] targets) : base(startValue, endValue, intensity, targets)
        {
        }

        public TweenShakePosition(Vector3 startValue, Vector3 endValue, float intensity, List<Transform> targets) : base(startValue, endValue, intensity, targets)
        {
        }


        protected override void SetEssentials()
        {
            TweenType = TweenType.ShakePosition;
            OnTweenValueUpdate(StartValue);
        }

        protected override void OnTweenValueUpdate(Vector3 lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.position = lerped;
            }
        }

        protected override void GetStartValue(Transform target)
        {
            StartValue = target.position;
        }
    }

    public class TweenShakeLocalPosition : TweenShakeVector3Targets<Transform>
    {
        public TweenShakeLocalPosition(Vector3 endValue, Vector3 intensity, Transform target) : base(endValue, intensity, target)
        {
        }

        public TweenShakeLocalPosition(Vector3 endValue, Vector3 intensity, params Transform[] targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalPosition(Vector3 endValue, Vector3 intensity, List<Transform> targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalPosition(Vector3 startValue, Vector3 endValue, Vector3 intensity, Transform target) : base(startValue, endValue, intensity, target)
        {
        }

        public TweenShakeLocalPosition(Vector3 startValue, Vector3 endValue, Vector3 intensity, params Transform[] targets) : base(startValue, endValue, intensity, targets)
        {
        }

        public TweenShakeLocalPosition(Vector3 startValue, Vector3 endValue, Vector3 intensity, List<Transform> targets) : base(startValue, endValue, intensity, targets)
        {
        }


        // float intensity
        public TweenShakeLocalPosition(Vector3 endValue, float intensity, Transform target) : base(endValue, intensity, target)
        {
        }

        public TweenShakeLocalPosition(Vector3 endValue, float intensity, params Transform[] targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalPosition(Vector3 endValue, float intensity, List<Transform> targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalPosition(Vector3 startValue, Vector3 endValue, float intensity, Transform target) : base(startValue, endValue, intensity, target)
        {
        }

        public TweenShakeLocalPosition(Vector3 startValue, Vector3 endValue, float intensity, params Transform[] targets) : base(startValue, endValue, intensity, targets)
        {
        }

        public TweenShakeLocalPosition(Vector3 startValue, Vector3 endValue, float intensity, List<Transform> targets) : base(startValue, endValue, intensity, targets)
        {
        }



        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.ShakeLocalPosition;
        }

        protected override void OnTweenValueUpdate(Vector3 lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.localPosition = lerped;
            }
        }

        protected override void GetStartValue(Transform target)
        {
            StartValue = target.localPosition;
        }
    }
}
