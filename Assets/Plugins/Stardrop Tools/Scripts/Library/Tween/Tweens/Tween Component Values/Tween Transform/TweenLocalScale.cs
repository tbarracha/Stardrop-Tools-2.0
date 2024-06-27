
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenLocalScale : TweenVector3Targets<Transform>
    {
        public TweenLocalScale(Vector3 endValue, Transform target) : base(endValue, target)
        {
        }

        public TweenLocalScale(Vector3 endValue, params Transform[] targets) : base(endValue, targets)
        {
        }

        public TweenLocalScale(Vector3 endValue, List<Transform> targets) : base(endValue, targets)
        {
        }

        public TweenLocalScale(Vector3 startValue, Vector3 endValue, Transform target) : base(startValue, endValue, target)
        {
        }

        public TweenLocalScale(Vector3 startValue, Vector3 endValue, params Transform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenLocalScale(Vector3 startValue, Vector3 endValue, List<Transform> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.LocalScale;
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
