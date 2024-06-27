
using System.Collections.Generic;

namespace StardropTools.Tween
{
    public abstract class TweenTargets<TValue, TTarget> : TweenValue<TValue>, ITweenTargets<TValue, TTarget>
    {
        protected readonly List<TTarget> targets = new List<TTarget>();
        protected TTarget target => GetTarget();

        // Constructor
        // ------------------------------------------------------------------------------
        protected TweenTargets(TValue startValue, TValue endValue, TTarget target)
        {
            StartValue = startValue;
            EndValue = endValue;

            AddTarget(target);
        }

        protected TweenTargets(TValue startValue, TValue endValue, params TTarget[] targets)
        {
            StartValue = startValue;
            EndValue = endValue;

            AddTargets(targets);
        }

        protected TweenTargets(TValue startValue, TValue endValue, List<TTarget> targets)
        {
            StartValue = startValue;
            EndValue = endValue;

            AddTargets(targets);
        }


        protected TweenTargets(TValue endValue, TTarget target)
        {
            EndValue = endValue;
            AddTarget(target);
            GetStartValue(target);
        }

        protected TweenTargets(TValue endValue, params TTarget[] targets)
        {
            EndValue = endValue;
            AddTargets(targets);
            GetStartValue(target);
        }

        protected TweenTargets(TValue endValue, List<TTarget> targets)
        {
            EndValue = endValue;
            AddTargets(targets);
            GetStartValue(target);
        }

        protected abstract void GetStartValue(TTarget target);


        // Targets
        // ------------------------------------------------------------------------------
        public TTarget GetTarget()
        {
            return targets.Count > 0 ? targets[0] : default(TTarget);
        }

        public List<TTarget> GetTargets()
        {
            return targets;
        }


        public bool Contains(TTarget target)
        {
            return targets.Contains(target);
        }


        public bool AddTarget(TTarget target)
        {
            if (!Contains(target))
            {
                targets.Add(target);
                return true;
            }

            return false;
        }

        public bool AddTargets(params TTarget[] targets)
        {
            bool addedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }

        public bool AddTargets(List<TTarget> targets)
        {
            bool addedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }


        public bool RemoveTarget(TTarget target)
        {
            return targets.Remove(target);
        }

        public bool RemoveTargets(params TTarget[] targets)
        {
            bool removedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (RemoveTarget(iteratedTarget))
                {
                    removedAny = true;
                }
            }

            return removedAny;
        }

        public bool RemoveTargets(List<TTarget> targets)
        {
            bool removedAny = false;

            foreach (TTarget iteratedTarget in targets)
            {
                if (RemoveTarget(iteratedTarget))
                {
                    removedAny = true;
                }
            }

            return removedAny;
        }


        public void ClearTargets()
        {
            targets.Clear();
        }

        protected override void TweenUpdate(float percent)
        {
            OnTweenUpdate();
        }

        protected virtual void OnTweenUpdate()
        {
            OnTweenValueUpdate(Lerped);
            OnTweenValue?.Invoke(Lerped);
        }

        protected abstract void OnTweenValueUpdate(TValue lerped);

        protected override void SetEssentials()
        {
            if (StartValueMode == StartValueMode.Eaguer)
                OnTweenValueUpdate(StartValue);
        }
    }
}
