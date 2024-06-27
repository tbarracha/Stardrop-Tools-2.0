
using System.Collections.Generic;

namespace StardropTools.Tween
{
    public abstract class TweenContainer<TValue, TTarget> : Tween, ITweenTargets<TValue, TTarget>
    {
        protected ITweenValue<TValue> tweenValue;

        public TValue StartValue { get; set; }
        public TValue EndValue { get; set; }
        public TValue Lerped { get; protected set; }

        public EventCallback<TValue> OnTweenValue { get; set; } = new EventCallback<TValue>();


        // Constructor
        // ------------------------------------------------------------------------------

        protected TweenContainer(TValue startValue, TValue endValue, TTarget target)
        {
            StartValue = startValue;
            EndValue = endValue;

            AddTarget(target);
        }

        protected TweenContainer(TValue startValue, TValue endValue, params TTarget[] targets)
        {
            StartValue = startValue;
            EndValue = endValue;

            AddTargets(targets);
        }

        protected TweenContainer(TValue startValue, TValue endValue, List<TTarget> targets)
        {
            StartValue = startValue;
            EndValue = endValue;

            AddTargets(targets);
        }


        protected TweenContainer(TValue endValue, TTarget target)
        {
            EndValue = endValue;
            AddTarget(target);
        }

        protected TweenContainer(TValue endValue, params TTarget[] targets)
        {
            EndValue = endValue;
            AddTargets(targets);
        }

        protected TweenContainer(TValue endValue, List<TTarget> targets)
        {
            EndValue = endValue;
            AddTargets(targets);
        }



        // Value
        // ------------------------------------------------------------------------------
        #region Value

        public ITween SetStartValue(TValue startValue)
        {
            StartValue = startValue;
            return this;
        }

        public ITween SetEndValue(TValue endValue)
        {
            EndValue = endValue;
            return this;
        }

        public ITween SetStartEndValue(TValue startValue, TValue endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
            return this;
        }



        protected override void OnLoop()
        {

        }

        protected override void OnPingPong()
        {
            TValue temp = StartValue;
            StartValue = EndValue;
            EndValue = temp;
        }

        protected override void OnRemovedFromManagerList()
        {
            OnTweenValue?.ClearAllSubscriptions();
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenValue?.Invoke(Lerped);
        }

        #endregion // Value


        // Targets
        // ------------------------------------------------------------------------------
        #region Targets

        protected readonly List<TTarget> targets = new List<TTarget>();
        protected TTarget target => GetTarget();



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

        #endregion // Targets
    }
}
