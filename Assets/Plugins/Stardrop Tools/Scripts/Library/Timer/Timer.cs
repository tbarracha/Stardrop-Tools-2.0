
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public class Timer
    {
        [SerializeField] protected TimerLoop loopType;
        [SerializeField] protected TimerState timerState;
        [SerializeField] protected float delay;
        [SerializeField] protected float duration;
        [Space]
        [SerializeField] protected float runtime;
        [SerializeField] protected float percent;

        protected bool ignoreTimeScale;
        private System.Action onCompleteCallback;

        public float Runtime => runtime;
        public float Percent => percent;


        #region Events
        public readonly CustomEvent OnTimerStart = new CustomEvent();
        public readonly CustomEvent OnTimerComplete = new CustomEvent();
        public readonly CustomEvent OnTimerUpdate = new CustomEvent();
        public readonly CustomEvent OnTimerPaused = new CustomEvent();
        public readonly CustomEvent OnTimerCanceled = new CustomEvent();

        public readonly CustomEvent<float> OnRuntime = new CustomEvent<float>();
        public readonly CustomEvent<float> OnPercent = new CustomEvent<float>();

        public readonly CustomEvent OnDelayStart = new CustomEvent();
        public readonly CustomEvent OnDelayComplete = new CustomEvent();

        #endregion // Events


        /// <summary>
        /// Creating a timer just creates it. To start it you need to call Initialize();
        /// </summary>
        public Timer(float duration, bool ignoreTimeScale = false)
        {
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;
        }


        /// <summary>
        /// Creating a timer just creates it. To start it you need to call Initialize();
        /// </summary>
        public Timer(float duration, TimerLoop loop, bool ignoreTimeScale = false)
        {
            this.duration = duration;
            this.loopType = loop;
            this.ignoreTimeScale = ignoreTimeScale;
        }


        /// <summary>
        /// Creating a timer just creates it. To start it you need to call Initialize();
        /// </summary>
        public Timer(float delay, float duration, bool ignoreTimeScale = false)
        {
            this.delay = delay;
            this.duration = duration;
            this.ignoreTimeScale = ignoreTimeScale;
        }


        /// <summary>
        /// Creating a timer just creates it. To start it you need to call Initialize();
        /// </summary>
        public Timer(float delay, float duration, TimerLoop loop, bool ignoreTimeScale = false)
        {
            this.delay = delay;
            this.duration = duration;
            this.loopType = loop;
            this.ignoreTimeScale = ignoreTimeScale;
        }


        public Timer Play()
        {
            ResetTimer();

            if (delay > 0)
                ChangeState(TimerState.Waiting);
            else
                ChangeState(TimerState.Running);

            TimerManager.Instance.AddTimer(this);
            OnTimerStart?.Invoke();

            return this;
        }

        public Timer Play(System.Action onComplete)
        {
            onCompleteCallback = onComplete;
            return Play();
        }

        public Timer Start() => Play();

        public void ChangeState(TimerState nextState)
        {
            // check if not the same
            if (timerState == nextState)
                return;

            timerState = nextState;

            // to delay
            if (nextState == TimerState.Waiting)
                OnDelayStart?.Invoke();

            // from delay to running
            if (timerState == TimerState.Waiting && nextState == TimerState.Running)
                OnDelayComplete?.Invoke();

            // to complete
            if (nextState == TimerState.Completed)
                OnTimerComplete?.Invoke();

            // to pause
            if (nextState == TimerState.Paused)
                OnTimerPaused?.Invoke();

            // to cancel
            if (nextState == TimerState.Canceled)
                OnTimerCanceled?.Invoke();

            ResetTimer();
        }

        public void Tick()
        {
            switch (timerState)
            {
                case TimerState.Waiting:
                    Waiting();
                    break;

                case TimerState.Running:
                    Running();
                    break;

                case TimerState.Completed:
                    Complete();
                    break;

                case TimerState.Paused:
                    Pause();
                    break;

                case TimerState.Canceled:
                    Stop();
                    break;
            }
        }

        protected virtual void Waiting()
        {
            if (ignoreTimeScale)
                runtime += Time.unscaledDeltaTime;
            else
                runtime += Time.deltaTime;

            if (runtime >= delay)
                ChangeState(TimerState.Running);
        }

        protected virtual void Running()
        {
            if (ignoreTimeScale)
                runtime += Time.unscaledDeltaTime;

            else
                runtime += Time.deltaTime;

            percent = Mathf.Min(runtime / duration, 1);

            OnRuntime?.Invoke(runtime);
            OnPercent?.Invoke(percent);

            if (percent >= 1)
                ChangeState(TimerState.Completed);

            OnTimerUpdate?.Invoke();
        }

        protected virtual void Complete()
        {
            onCompleteCallback?.Invoke();

            if (loopType == TimerLoop.None)
            {
                onCompleteCallback = null;
                RemoveFromManagerList();
            }

            else if (loopType == TimerLoop.Loop)
                Loop();
        }

        public virtual void Pause() { }

        public virtual void Cancel() => ChangeState(TimerState.Canceled);

        public virtual void Stop()
            => RemoveFromManagerList();

        protected void Loop()
        {
            ResetTimer();
            ChangeState(TimerState.Running);
        }

        protected void RemoveFromManagerList()
        {
            OnTimerStart    ?.ClearAllListeners();
            OnTimerUpdate   ?.ClearAllListeners();
            OnTimerComplete ?.ClearAllListeners();
            OnTimerPaused   ?.ClearAllListeners();
            OnTimerCanceled ?.ClearAllListeners();
            OnDelayStart    ?.ClearAllListeners();
            OnDelayComplete ?.ClearAllListeners();

            TimerManager.Instance.RemoveTimer(this);
        }

        public void ResetTimer()
        {
            runtime = 0;
        }
    }
}