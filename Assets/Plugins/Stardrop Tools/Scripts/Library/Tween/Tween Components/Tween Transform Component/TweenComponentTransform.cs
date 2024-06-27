
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace StardropTools.Tween.Components
{
    public abstract class TweenComponentTransform<TweenedValueType> : TweenComponent, ITweenTargets<TweenedValueType, Transform>
    {
        [Header("Values")]
        [SerializeField] protected SimulationSpace simulationSpace;
        [SerializeField] protected TweenedValueType startValue;
        [SerializeField] protected TweenedValueType endValue;
        [SerializeField] protected bool uniformStart;
        [SerializeField] protected bool uniformEnd;
        [Space]
        [SerializeField] protected bool hasStart;

        [Header("Targets")]
        [SerializeField] protected List<Transform> targets = new List<Transform>();

        [Header("Unity Event")]
        [SerializeField] protected UnityEvent<TweenedValueType> onTweenValue;

        protected Transform target => GetTarget();
        protected ITweenValue<TweenedValueType> tweenValue => tween as ITweenValue<TweenedValueType>;

        public TweenedValueType StartValue { get => startValue; set => startValue = value; }
        public TweenedValueType EndValue { get => endValue; set => endValue = value; }
        public bool HasStart { get => hasStart; set => hasStart = value; }
        public SimulationSpace SimulationSpace { get => simulationSpace; set => simulationSpace = value; }
        public EventCallback<TweenedValueType> OnTweenValue { get; set; } = new EventCallback<TweenedValueType>();


        [NaughtyAttributes.ShowNativeProperty]
        public TweenedValueType Lerped { get; protected set; }


        protected override void SetTweenEssentials()
        {
            base.SetTweenEssentials();

            if (tweenValue != null && tweenValue.OnTweenValue == null)
            {
                tweenValue.OnTweenValue = new EventCallback<TweenedValueType>();
            }
            
            tweenValue?.OnTweenValue?.Subscribe(OnTweenValueChanged);
        }


        // Tween
        // ---------------------------------------------------------------------------------------
        public new int TweenID { get => tween.TweenID; set => tween.TweenID = value; }

        public TweenType TweenType => tween != null ? tween.TweenType : TweenType.None;

        public TweenState TweenState => tween != null ? tween.TweenState : TweenState.Idle;

        public bool IsInManagerList { get; set; }



        // Value
        // ---------------------------------------------------------------------------------------
        public ITween SetStartValue(TweenedValueType startValue)
        {
            StartValue = startValue;
            return this;
        }

        public ITween SetEndValue(TweenedValueType endValue)
        {
            EndValue = endValue;
            return this;
        }

        public ITween SetStartEndValue(TweenedValueType startValue, TweenedValueType endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
            return this;
        }


        protected virtual void OnTweenValueChanged(TweenedValueType lerped)
        {
            onTweenValue?.Invoke(lerped);
        }



        // Target
        // ---------------------------------------------------------------------------------------
        public Transform GetTarget()
        {
            return targets.Count > 0 ? targets[0] : default(Transform);
        }

        public List<Transform> GetTargets()
        {
            return targets;
        }


        public bool Contains(Transform target)
        {
            return targets.Contains(target);
        }


        public bool AddTarget(Transform target)
        {
            if (!Contains(target))
            {
                targets.Add(target);
                return true;
            }

            return false;
        }

        public bool AddTargets(params Transform[] targets)
        {
            bool addedAny = false;

            foreach (Transform iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }

        public bool AddTargets(List<Transform> targets)
        {
            bool addedAny = false;

            foreach (Transform iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }


        public bool RemoveTarget(Transform target)
        {
            return targets.Remove(target);
        }

        public bool RemoveTargets(params Transform[] targets)
        {
            bool removedAny = false;

            foreach (Transform iteratedTarget in targets)
            {
                if (RemoveTarget(iteratedTarget))
                {
                    removedAny = true;
                }
            }

            return removedAny;
        }

        public bool RemoveTargets(List<Transform> targets)
        {
            bool removedAny = false;

            foreach (Transform iteratedTarget in targets)
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

        public void UpdateTweenState() { }

        protected virtual void ValidateStartEndValues()
        {
            
        }
    }
}
