
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenComponent : MonoBehaviour, ITweenable
    {
        [Header("Tween Properties")]
        [SerializeField]
        protected int tweenIDMultiplier;
        [SerializeField]
        protected EaseType easeType;
        [SerializeField]
        protected LoopType loopType;
        [SerializeField, NaughtyAttributes.ShowIf("showCurve")]
        protected AnimationCurve easeCurve;
        [SerializeField]
        protected float duration;
        [SerializeField]
        protected float delay;
        [SerializeField, NaughtyAttributes.ShowIf("showLoopCount")]
        protected int loopCount;
        [SerializeField]
        protected bool ignoreTimeScale;

        [SerializeField]
        protected ITween tween;

        public int TweenID { get => tween != null ? tween.TweenID : -1; set => tween.TweenID = value; }

        public bool showCurve {  get; protected set; }
        public bool showLoopCount { get; protected set; }

        public EaseType EaseType
        {
            get => easeType;
            set => easeType = value;
        }

        public LoopType LoopType
        {
            get => loopType;
            set => loopType = value;
        }

        public AnimationCurve EaseCurve
        {
            get => easeCurve;
            set => easeCurve = value;
        }

        public float Duration
        {
            get => duration;
            set => duration = value;
        }

        public float Delay
        {
            get => delay;
            set => delay = value;
        }

        public float TotalDuration => duration + delay;

        public int LoopCount
        {
            get => loopCount;
            set => loopCount = value;
        }

        public bool IgnoreTimeScale
        {
            get => ignoreTimeScale;
            set => ignoreTimeScale = value;
        }

        public float Percent => tween != null ? tween.Percent : 0;


        public Tween GetTween() => tween.GetTween();

        public void GenerateRandomTweenIDMultiplier()
        {
            tweenIDMultiplier = (int)Random.Range(int.MinValue * .5f, int.MaxValue * .5f);
        }

        public virtual ITween Play(System.Action onPlayCallback)
        {
            Stop();
            CreateTween();
            tween.Play(onPlayCallback);
            return tween;
        }

        public virtual ITween Play()
        {
            Stop();
            CreateTween();
            tween.Play();
            return tween;
        }

        public void Stop()
        {
            tween?.Stop();
        }

        public void Pause()
        {
            tween?.Pause();
        }

        protected virtual void SetTweenEssentials()
        {
            tween.EaseType = EaseType;
            tween.LoopType = LoopType;
            tween.Duration = Duration;
            tween.Delay = Delay;

            if (EaseType == EaseType.AnimationCurve)
                tween.EaseCurve = EaseCurve;

            if (LoopType != LoopType.None && loopCount > 0)
                tween.LoopCount = LoopCount;
        }

        protected abstract void CreateTween();


#if UNITY_EDITOR
        [NaughtyAttributes.Button("Start Tween")]
        private void TweenStart()
        {
            if (Application.isPlaying)
                Play();
        }

        protected virtual void OnValidate()
        {
            showCurve       = EaseType == EaseType.AnimationCurve;
            showLoopCount   = LoopType != LoopType.None;
        }
#endif
    }
}