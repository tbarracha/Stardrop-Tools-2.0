
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Tweens MUST always end with .Initialize();
    /// </summary>
    public abstract class Tween
    {
        protected int tweenID;
        protected TweenType tweenType;
        [Space]
        protected EaseType easeType;
        protected LoopType loopType;
        [Space]
        protected float duration;
        protected float delay;
        protected float percent;
        [Space]
        protected int loopCount;
        protected bool ignoreTimeScale;
        [Space]
        protected AnimationCurve easeCurve;

        protected TweenState tweenState;
        protected float runtime;
        protected bool isValid;

        #region Parameters

        public int TweenID => tweenID;
        public TweenType TweenType => tweenType;

        public float Duration => duration;
        public float Delay => delay;
        public float TotalDuration => duration + delay;

        #endregion // Params


        #region Events

        public readonly BaseEvent OnTweenStart = new BaseEvent();
        public readonly BaseEvent OnTweenComplete = new BaseEvent();
        public readonly BaseEvent OnTweenUpdate = new BaseEvent();
        public readonly BaseEvent OnTweenPaused = new BaseEvent();
        public readonly BaseEvent OnTweenCanceled = new BaseEvent();

        public readonly BaseEvent OnDelayStart = new BaseEvent();
        public readonly BaseEvent OnDelayComplete = new BaseEvent();

        #endregion // Events



        #region Setters

        public Tween SetID(int tweenID)
        {
            this.tweenID = tweenID;
            return this;
        }

        public Tween SetType(TweenType tweenType)
        {
            this.tweenType = tweenType;
            return this;
        }

        public Tween SetIdTag(int tweenID, TweenType tweenType)
        {
            this.tweenID = tweenID;
            this.tweenType = tweenType;
            return this;
        }


        public Tween SetEaseType(EaseType easeType)
        {
            this.easeType = easeType;
            return this;
        }

        public Tween SetLoopType(LoopType loopType)
        {
            this.loopType = loopType;
            return this;
        }        

        public Tween SetEaseAndLoop(EaseType easeType, LoopType loopType)
        {
            this.easeType = easeType;
            this.loopType = loopType;
            return this;
        }

        public Tween SetAnimationCurve(AnimationCurve easeCurve)
        {
            this.easeCurve = easeCurve;
            return this;
        }


        public Tween SetDuration(float duration)
        {
            this.duration = duration;
            return this;
        }

        public Tween SetDelay(float delay)
        {
            this.delay = delay;
            return this;
        }

        public Tween SetDurationAndDelay(float duration, float delay)
        {
            this.duration = duration;
            this.delay = delay;
            return this;
        }

        public Tween SetIgnoreTimeScale(bool ignoreTimeScale)
        {
            this.ignoreTimeScale = ignoreTimeScale;
            return this;
        }

        public Tween SetLoopCount(int loopCount)
        {
            this.loopCount = loopCount;
            return this;
        }

        #endregion // Setters


        public Tween Initialize()
        {
            ResetRuntime();

            if (delay > 0)
                ChangeState(TweenState.waiting);
            else
                ChangeState(TweenState.running);

            isValid = TweenManager.Instance.ProcessTween(this);

            if (isValid == true)
                OnTweenStart?.Invoke();

            return this;
        }


        public void UpdateTweenState()
        {
            switch (tweenState)
            {
                case TweenState.waiting:
                    Waiting();
                    break;

                case TweenState.running:
                    Running();
                    break;

                case TweenState.complete:
                    Complete();
                    break;

                case TweenState.paused:
                    Pause();
                    break;

                case TweenState.canceled:
                    Stop();
                    break;
            }
        }


        public void ChangeState(TweenState nextState)
        {
            // check if not the same
            if (tweenState == nextState)
                return;

            // to delay
            if (nextState == TweenState.waiting)
                OnDelayStart?.Invoke();

            // from delay to running
            if (tweenState == TweenState.waiting && nextState == TweenState.running)
                OnDelayComplete?.Invoke();

            // to complete
            if (nextState == TweenState.complete)
                OnTweenComplete?.Invoke();
            
            // to pause
            if (nextState == TweenState.paused)
                OnTweenPaused?.Invoke();

            // to cancel
            if (nextState == TweenState.canceled)
                OnTweenCanceled?.Invoke();

            tweenState = nextState;
            ResetRuntime();
        }

        protected virtual void Waiting()
        {
            if (ignoreTimeScale)
                runtime += Time.unscaledDeltaTime;
            else
                runtime += Time.deltaTime;

            if (runtime >= delay)
                ChangeState(TweenState.running);
        }

        protected virtual void Running()
        {
            if (ignoreTimeScale)
                runtime += Time.unscaledDeltaTime;

            else
                runtime += Time.deltaTime;

            percent = Mathf.Min(runtime / duration, 1);

            TweenUpdate(percent);

            if (percent >= 1)
                ChangeState(TweenState.complete);

            OnTweenUpdate?.Invoke();
        }

        protected virtual void Complete()
        {
            if (loopType == LoopType.None)
                RemoveFromManagerList();

            else if (loopType == LoopType.Loop)
                Loop();

            else if (loopType == LoopType.PingPong)
                PingPong();
        }

        public virtual void Pause() { }

        public virtual void Stop()
            => RemoveFromManagerList();

        protected float Ease(float percent)
        {
            if (easeType != EaseType.AnimationCurve)
                return TweenEase.Ease(easeType, percent);

            else
            {
                if (easeCurve.keys.Length < 2)
                {
                    Debug.Log("Tween Animation Curve needs more keys!");
                    return TweenEase.Ease(EaseType.Linear, percent);
                }

                return easeCurve.Evaluate(percent);
            }
        }

        protected abstract void TweenUpdate(float percent);
        protected abstract void Loop();
        protected abstract void PingPong();

        protected void RemoveFromManagerList()
        {
            OnTweenStart.RemoveAllListeners();
            OnTweenUpdate.RemoveAllListeners();
            OnTweenComplete.RemoveAllListeners();
            OnTweenPaused.RemoveAllListeners();
            OnTweenCanceled.RemoveAllListeners();
            OnDelayStart.RemoveAllListeners();
            OnDelayComplete.RemoveAllListeners();

            TweenManager.Instance.RemoveTween(this);
        }

        protected void ResetRuntime() => runtime = 0;
    }
}