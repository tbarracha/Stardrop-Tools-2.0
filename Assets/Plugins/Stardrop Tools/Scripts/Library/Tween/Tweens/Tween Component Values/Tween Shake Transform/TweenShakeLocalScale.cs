using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeLocalScale : TweenShakeVector3Targets<Transform>
    {
        public TweenShakeLocalScale(Vector3 endValue, Vector3 intensity, Transform target) : base(endValue, intensity, target)
        {
        }

        public TweenShakeLocalScale(Vector3 endValue, Vector3 intensity, params Transform[] targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalScale(Vector3 endValue, Vector3 intensity, List<Transform> targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalScale(Vector3 startValue, Vector3 endValue, Vector3 intensity, Transform target) : base(startValue, endValue, intensity, target)
        {
        }

        public TweenShakeLocalScale(Vector3 startValue, Vector3 endValue, Vector3 intensity, params Transform[] targets) : base(startValue, endValue, intensity, targets)
        {
        }

        public TweenShakeLocalScale(Vector3 startValue, Vector3 endValue, Vector3 intensity, List<Transform> targets) : base(startValue, endValue, intensity, targets)
        {
        }


        // float intensity
        public TweenShakeLocalScale(Vector3 endValue, float intensity, Transform target) : base(endValue, intensity, target)
        {
        }

        public TweenShakeLocalScale(Vector3 endValue, float intensity, params Transform[] targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalScale(Vector3 endValue, float intensity, List<Transform> targets) : base(endValue, intensity, targets)
        {
        }

        public TweenShakeLocalScale(Vector3 startValue, Vector3 endValue, float intensity, Transform target) : base(startValue, endValue, intensity, target)
        {
        }

        public TweenShakeLocalScale(Vector3 startValue, Vector3 endValue, float intensity, params Transform[] targets) : base(startValue, endValue, intensity, targets)
        {
        }

        public TweenShakeLocalScale(Vector3 startValue, Vector3 endValue, float intensity, List<Transform> targets) : base(startValue, endValue, intensity, targets)
        {
        }



        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.ShakeLocalScale;
        }

        protected override void OnTweenValueUpdate(Vector3 lerped)
        {
            foreach (var item in targets)
            {
                if (item == null)
                    continue;

                item.localScale = lerped;
            }
        }

        protected override void GetStartValue(Transform target)
        {
            StartValue = target.localScale;
        }
    }
}
