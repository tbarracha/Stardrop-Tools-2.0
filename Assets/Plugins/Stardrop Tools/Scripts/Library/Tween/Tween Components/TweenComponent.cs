
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenComponent : MonoBehaviour
    {
        [Header("Tweens To Sequence")]
        [SerializeField] protected TweenComponent[] tweensToPlayOnComplete;
        [SerializeField] protected TweenComponent[] tweensToPlayOnDelayCompleted;

        [Header("Tween Properties")]
        public EaseType easeType;
        public LoopType loopType;
        [NaughtyAttributes.ShowIf("showCurve")]
        public AnimationCurve curve; // only show this if easeType is set to AnimationCurve
        [NaughtyAttributes.ShowIf("showLoopCount")]
        public int loopCount;
        public float delay;
        public float duration;
        public bool hasStart;

        protected Tween tween;
        protected System.Action OnPlayFinishedCallback;

#if UNITY_EDITOR
        protected bool showCurve, showLoopCount;
#endif

        public CustomEvent OnDelayCompleted => tween.OnDelayCompleted;
        public CustomEvent OnTweenCompleted => tween.OnTweenCompleted;
        public CustomEvent OnTweenPaused    => tween.OnTweenPaused;
        public CustomEvent OnTweenCanceled  => tween.OnTweenCanceled;


        public abstract Tween Play();
        public Tween Play(System.Action onPlayFinishedCallback)
        {
            OnPlayFinishedCallback = onPlayFinishedCallback;
            Play();

            return tween;
        }

        public void Stop()
        {
            if (tween != null)
                tween.Stop();
        }

        public void Pause()
        {
            if (tween != null)
                tween.Pause();
        }

        protected virtual void SetTweenEssentials()
        {
            tween.SetID(this);

            tween.SetEaseType(easeType)
                .SetLoopType(loopType)
                .SetLoopCount(loopCount)
                .SetDurationAndDelay(duration, delay);

            if (easeType == EaseType.AnimationCurve)
                tween.SetAnimationCurve(curve);

            tween.OnTweenCompleted.AddListener(PlayTweensOnComplete);
            tween.OnDelayCompleted.AddListener(PlayTweensOnDelayComplete);
        }

        protected void PlayTweensOnComplete()
        {
            OnPlayFinishedCallback?.Invoke();
            OnPlayFinishedCallback = null;

            if (tweensToPlayOnComplete.Exists())
                for (int i = 0; i < tweensToPlayOnComplete.Length; i++)
                    tweensToPlayOnComplete[i].Play();
        }

        protected void PlayTweensOnDelayComplete()
        {
            if (tweensToPlayOnDelayCompleted.Exists())
                for (int i = 0; i < tweensToPlayOnDelayCompleted.Length; i++)
                    tweensToPlayOnDelayCompleted[i].Play();
        }



#if UNITY_EDITOR
        [NaughtyAttributes.Button("Start Tween")]
        private void TweenStart()
        {
            if (Application.isPlaying)
                Play();
        }

        protected virtual void OnValidate()
        {
            showCurve       = easeType == EaseType.AnimationCurve ? true : false;
            showLoopCount   = loopType != LoopType.None;
        }
#endif
    }
}