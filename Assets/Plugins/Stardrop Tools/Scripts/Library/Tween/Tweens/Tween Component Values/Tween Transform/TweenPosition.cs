
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenPosition : TweenVector3Targets<Transform>
    {
        public TweenPosition(Vector3 startValue, Vector3 endValue, Transform target) : base(startValue, endValue, target)
        {
        }

        public TweenPosition(Vector3 startValue, Vector3 endValue, List<Transform> targets) : base(startValue, endValue, targets)
        {
        }

        public TweenPosition(Vector3 startValue, Vector3 endValue, params Transform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenPosition(Vector3 endValue, Transform target) : base(endValue, target)
        {
        }

        public TweenPosition(Vector3 endValue, params Transform[] targets) : base(endValue, targets)
        {
        }

        public TweenPosition(Vector3 endValue, List<Transform> targets) : base(endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            TweenType = TweenType.Position;
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
            base.SetEssentials();
            StartValue = target.position;
        }
    }

    public class TweenLocalPosition : TweenVector3Targets<Transform>
    {
        public TweenLocalPosition(Vector3 endValue, Transform target) : base(endValue, target)
        {
        }

        public TweenLocalPosition(Vector3 endValue, params Transform[] targets) : base(endValue, targets)
        {
        }

        public TweenLocalPosition(Vector3 endValue, List<Transform> targets) : base(endValue, targets)
        {
        }

        public TweenLocalPosition(Vector3 startValue, Vector3 endValue, Transform target) : base(startValue, endValue, target)
        {
        }

        public TweenLocalPosition(Vector3 startValue, Vector3 endValue, params Transform[] targets) : base(startValue, endValue, targets)
        {
        }

        public TweenLocalPosition(Vector3 startValue, Vector3 endValue, List<Transform> targets) : base(startValue, endValue, targets)
        {
        }


        protected override void SetEssentials()
        {
            base.SetEssentials();
            TweenType = TweenType.LocalPosition;
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
