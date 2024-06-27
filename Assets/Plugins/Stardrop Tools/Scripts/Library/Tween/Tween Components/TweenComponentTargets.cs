
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

namespace StardropTools.Tween.Components
{
    public abstract class TweenComponentTargets<TweenValue, TweenTargetType> : TweenComponent, ITweenTargets<TweenValue, TweenTargetType>
    {
        [Header("Values")]
        [SerializeField] protected TweenValue startValue;
        [SerializeField] protected TweenValue endValue;
        [NaughtyAttributes.ShowIf("ShowIfVector")]
        [SerializeField] protected bool uniformStart;
        [NaughtyAttributes.ShowIf("ShowIfVector")]
        [SerializeField] protected bool uniformEnd;
        [Space]
        [SerializeField] protected bool hasStart;

        [Header("Targets")]
        [SerializeField] protected List<TweenTargetType> targets = new List<TweenTargetType>();

        [Header("Unity Event")]
        [SerializeField] protected UnityEvent<TweenValue> onTweenValue;

        protected TweenTargetType target => GetTarget();
        protected ITweenValue<TweenValue> tweenValue => tween as ITweenValue<TweenValue>;

        public TweenValue StartValue { get => startValue; set => startValue = value; }
        public TweenValue EndValue { get => endValue; set => endValue = value; }
        public bool HasStart { get => hasStart; set => hasStart = value; }
        public EventCallback<TweenValue> OnTweenValue { get; set; } = new EventCallback<TweenValue>();


        [NaughtyAttributes.ShowNativeProperty]
        public TweenValue Lerped { get; protected set; }


        protected override void SetTweenEssentials()
        {
            base.SetTweenEssentials();

            if (tweenValue != null && tweenValue.OnTweenValue == null)
            {
                tweenValue.TweenID = tweenIDMultiplier;

                tweenValue.OnTweenValue = new EventCallback<TweenValue>();
                tweenValue?.OnTweenValue?.Subscribe(OnTweenValueChanged);
            }
        }


        // Tween
        // ---------------------------------------------------------------------------------------

        public new int TweenID { get => tween.TweenID; set => tween.TweenID = value; }

        public TweenType TweenType => tween != null ? tween.TweenType : TweenType.None;

        public TweenState TweenState => tween != null ? tween.TweenState : TweenState.Idle;

        public bool IsInManagerList { get; set; }
        int ITween.TweenID { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }



        // Value
        // ---------------------------------------------------------------------------------------
        public ITween SetStartValue(TweenValue startValue)
        {
            StartValue = startValue;
            return this;
        }

        public ITween SetEndValue(TweenValue endValue)
        {
            EndValue = endValue;
            return this;
        }

        public ITween SetStartEndValue(TweenValue startValue, TweenValue endValue)
        {
            StartValue = startValue;
            EndValue = endValue;
            return this;
        }


        protected virtual void OnTweenValueChanged(TweenValue lerped)
        {
            onTweenValue?.Invoke(lerped);
        }



        // Target
        // ---------------------------------------------------------------------------------------
        public TweenTargetType GetTarget()
        {
            return targets.Count > 0 ? targets[0] : default(TweenTargetType);
        }

        public List<TweenTargetType> GetTargets()
        {
            return targets;
        }


        public bool Contains(TweenTargetType target)
        {
            return targets.Contains(target);
        }


        public bool AddTarget(TweenTargetType target)
        {
            if (!Contains(target))
            {
                targets.Add(target);
                return true;
            }

            return false;
        }

        public bool AddTargets(params TweenTargetType[] targets)
        {
            bool addedAny = false;

            foreach (TweenTargetType iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }

        public bool AddTargets(List<TweenTargetType> targets)
        {
            bool addedAny = false;

            foreach (TweenTargetType iteratedTarget in targets)
            {
                if (AddTarget(iteratedTarget))
                {
                    addedAny = true;
                }
            }

            return addedAny;
        }


        public bool RemoveTarget(TweenTargetType target)
        {
            return targets.Remove(target);
        }

        public bool RemoveTargets(params TweenTargetType[] targets)
        {
            bool removedAny = false;

            foreach (TweenTargetType iteratedTarget in targets)
            {
                if (RemoveTarget(iteratedTarget))
                {
                    removedAny = true;
                }
            }

            return removedAny;
        }

        public bool RemoveTargets(List<TweenTargetType> targets)
        {
            bool removedAny = false;

            foreach (TweenTargetType iteratedTarget in targets)
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

        protected bool ShowIfVector()
        {
            if (startValue is Vector2 ||
                startValue is Vector3)
            {
                return true;
            }

            return false;
        }
    }
}
