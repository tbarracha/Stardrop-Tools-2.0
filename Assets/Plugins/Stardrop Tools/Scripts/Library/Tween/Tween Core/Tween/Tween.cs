using System;
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// Start   =>  tween.Play(); <para>
    /// Stop    =>  tween.Stop(); </para>
    /// </summary>
    public abstract class Tween : ITween
    {
        public TweenType TweenType { get; set; }

        public StartValueMode StartValueMode { get; set; }
        public EaseType EaseType { get; set; }
        public LoopType LoopType { get; set; }
        public AnimationCurve EaseCurve { get; set; }

        public float Duration { get; set; }
        public float Delay { get; set; }
        public float TotalDuration => Duration + Delay;
        
        public int LoopCount { get; set; }
        public bool IgnoreTimeScale { get; set; }
        public bool IsInManagerList { get; set; }

        public int TweenID { get; set; } = -1;
        public float Percent { get; protected set; }
        public TweenState TweenState { get; private set; }


        protected float runtime;
        protected bool isValid;
        protected int loops;
        protected int pingPongLoops;

        protected Action OnPlayCallBack;


        #region Events

        public readonly EventCallback OnTweenStarted      = new EventCallback();
        public readonly EventCallback OnTweenCompleted    = new EventCallback();
        public readonly EventCallback OnTweenTick         = new EventCallback();
        public readonly EventCallback OnTweenPaused       = new EventCallback();
        public readonly EventCallback OnTweenCanceled     = new EventCallback();

        public readonly EventCallback OnDelayStarted      = new EventCallback();
        public readonly EventCallback OnDelayCompleted    = new EventCallback();

        public readonly EventCallback<float> OnTweenPercentDuration = new EventCallback<float>();

        #endregion // Events



        #region Get Tween Type

        // Values
        public TweenFloat           asFloat         => this as TweenFloat;
        public TweenVector2         asVector2       => this as TweenVector2;
        public TweenVector3         asVector3       => this as TweenVector3;
        public TweenVector4         asVector4       => this as TweenVector4;
        public TweenQuaternion      asQuaternion    => this as TweenQuaternion;
        public TweenColor           asColor         => this as TweenColor;
        public TweenColorOpacity    asColorOpacity  => this as TweenColorOpacity;

        // Shake Values
        // public TweenShakeFloat      asShakeFloat    => this as TweenShakeFloat;
        // public TweenShakeVector2    asShakeVector2  => this as TweenShakeVector2;
        // public TweenShakeVector3    asShakeVector3  => this as TweenShakeVector3;
        // public TweenShakeVector4    asShakeVector4  => this as TweenShakeVector4;

        #endregion // Get tween type


        #region Setters

        public Tween SetID(int tweenID)
        {
            this.TweenID = tweenID;
            return this;
        }

        public Tween SetID(GameObject uniqueObject)
        {
            TweenID = uniqueObject.GetInstanceID();
            return this;
        }

        public Tween SetID<T>(T component) where T : Component
            => SetID(component.GetInstanceID());

        public Tween SetType(TweenType tweenType)
        {
            this.TweenType = tweenType;
            return this;
        }

        public Tween SetIdAndType(int tweenID, TweenType tweenType)
        {
            this.TweenID = tweenID;
            this.TweenType = tweenType;
            return this;
        }

        public Tween SetStartValueMode(StartValueMode startValueMode)
        {
            this.StartValueMode = startValueMode;
            return this;
        }

        public Tween SetEaseType(EaseType easeType)
        {
            this.EaseType = easeType;
            return this;
        }

        public Tween SetLoopType(LoopType loopType)
        {
            this.LoopType = loopType;
            return this;
        }        

        public Tween SetEaseAndLoop(EaseType easeType, LoopType loopType)
        {
            this.EaseType = easeType;
            this.LoopType = loopType;
            return this;
        }

        public Tween SetAnimationCurve(AnimationCurve easeCurve)
        {
            this.EaseCurve = easeCurve;
            return this;
        }


        public Tween SetDuration(float duration)
        {
            this.Duration = duration;
            return this;
        }

        public Tween SetDelay(float delay)
        {
            this.Delay = delay;
            return this;
        }

        public Tween SetDurationAndDelay(float duration, float delay)
        {
            this.Duration = duration;
            this.Delay = delay;
            return this;
        }

        public Tween SetIgnoreTimeScale(bool ignoreTimeScale)
        {
            this.IgnoreTimeScale = ignoreTimeScale;
            return this;
        }

        public Tween SetLoopCount(int loopCount)
        {
            this.LoopCount = loopCount;
            return this;
        }

        #endregion // Setters



        // API public
        // ---------------------------------------------------------------------

        public Tween GetTween() => this;

        public ITween Play()
        {
            if (TweenState == TweenState.Idle)
            {
                ResetRuntime();

                if (Delay > 0)
                    ChangeState(TweenState.Idle);
                else
                    ChangeState(TweenState.Running);

                isValid = TweenManager.Instance.ProcessTween(this);

                if (isValid)
                    OnTweenStarted?.Invoke();
            }
            
            if (TweenState == TweenState.Paused)
            {
                ChangeState(TweenState.Running);
            }

            return this;
        }

        public ITween Play(Action onPlayCallback)
        {
            OnPlayCallBack = onPlayCallback;
            return Play();
        }

        public ITween PlayAndStop(params ITween[] tweensToStop)
        {
            foreach (var tween in tweensToStop)
            {
                tween.Stop();
            }

            return Play();
        }

        public ITween PlayAndStop(Action onPlayCallback, params ITween[] tweensToStop)
        {
            foreach (var tween in tweensToStop)
            {
                tween.Stop();
            }

            return Play(onPlayCallback);
        }

        public virtual void Stop()
        {
            RemoveFromManagerList();
        }

        public virtual void Pause()
        {
            ChangeState(TweenState.Paused);
        }


        public void UpdateTweenState()
        {
            switch (TweenState)
            {
                case TweenState.Idle:
                    Waiting();
                    break;

                case TweenState.Running:
                    Running();
                    break;

                case TweenState.Complete:
                    Complete();
                    break;

                case TweenState.Paused:
                    Pause();
                    break;

                case TweenState.Canceled:
                    Stop();
                    break;
            }
        }


        public ITweenValue<TValue> TryGetAsTweenValue<TValue>()
        {
            try
            {
                return this as ITweenValue<TValue>;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }

        public ITweenTargets<TValue, TTarget> TryGetAsTweenTargets<TValue, TTarget>()
        {
            try
            {
                return this as ITweenTargets<TValue, TTarget>;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }




        // API internal
        // ---------------------------------------------------------------------

        protected void ChangeState(TweenState nextState)
        {
            // check if not the same
            if (TweenState == nextState)
                return;

            // to delay
            if (nextState == TweenState.Idle)
                OnDelayStarted?.Invoke();

            // from delay to running
            if (TweenState == TweenState.Idle && nextState == TweenState.Running)
                OnDelayCompleted?.Invoke();

            // to complete
            if (nextState == TweenState.Complete)
                OnTweenCompleted?.Invoke();
            
            // to pause
            if (nextState == TweenState.Paused)
                OnTweenPaused?.Invoke();

            // to cancel
            if (nextState == TweenState.Canceled)
                OnTweenCanceled?.Invoke();

            TweenState = nextState;
            ResetRuntime();
        }

        protected virtual void Waiting()
        {
            if (IgnoreTimeScale)
                runtime += Time.unscaledDeltaTime;
            else
                runtime += Time.deltaTime;

            if (runtime >= Delay)
                ChangeState(TweenState.Running);
        }

        protected virtual void Running()
        {
            if (IgnoreTimeScale)
                runtime += Time.unscaledDeltaTime;

            else
                runtime += Time.deltaTime;

            Percent = Mathf.Min(runtime / Duration, 1);

            TweenUpdate(Percent);
            OnTweenPercentDuration?.Invoke(Percent);

            if (Percent >= 1)
                ChangeState(TweenState.Complete);

            OnTweenTick?.Invoke();
        }

        protected virtual void Complete()
        {
            if (OnPlayCallBack != null)
            {
                OnPlayCallBack.Invoke();
                OnPlayCallBack = null;
            }
            

            if (LoopType == LoopType.None)
                RemoveFromManagerList();

            if (LoopType == LoopType.Loop)
                Loop();

            if (LoopType == LoopType.PingPong)
                PingPong();
        }

        protected float Ease(float percent)
        {
            if (EaseType != EaseType.AnimationCurve)
                return EaseValue.Ease(EaseType, percent);

            else
            {
                if (!EaseCurve.keys.Exists(2))
                {
                    Debug.Log("Tween Animation Curve needs more keys!");
                    return EaseValue.Ease(EaseType.Linear, percent);
                }

                return EaseCurve.Evaluate(percent);
            }
        }

        protected abstract void SetEssentials();
        protected abstract void TweenUpdate(float percent);

        protected void Loop()
        {
            OnLoop();
            LoopEssentials();
        }

        protected void PingPong()
        {
            OnPingPong();
            LoopEssentials();
        }

        private void LoopEssentials()
        {
            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }

        protected void LoopIncrement()
        {
            if (LoopCount <= 0)
                return;

            if (LoopType == LoopType.Loop)
            {
                loops++;
            }
            
            if (LoopType == LoopType.PingPong)
            {
                pingPongLoops++;

                if (pingPongLoops >= 2)
                {
                    loops++;
                    pingPongLoops = 0;
                }
            }

            if (loops >= LoopCount)
            {
                ResetLoopCount();
                Stop();
            }
        }

        protected void ResetLoopCount()
        {
            loops = 0;
            pingPongLoops = 0;
        }

        protected void ResetRuntime() => runtime = 0;


        protected virtual void OnLoop() { }
        protected virtual void OnPingPong() { }
        protected virtual void OnRemovedFromManagerList() { }


        protected void RemoveFromManagerList()
        {
            OnRemovedFromManagerList();

            OnTweenStarted      ?.ClearAllSubscriptions();
            OnTweenTick         ?.ClearAllSubscriptions();
            OnTweenCompleted    ?.ClearAllSubscriptions();
            OnTweenPaused       ?.ClearAllSubscriptions();
            OnTweenCanceled     ?.ClearAllSubscriptions();
            OnDelayStarted      ?.ClearAllSubscriptions();
            OnDelayCompleted    ?.ClearAllSubscriptions();

            TweenManager.Instance.RemoveTween(this);
        }
    }
}