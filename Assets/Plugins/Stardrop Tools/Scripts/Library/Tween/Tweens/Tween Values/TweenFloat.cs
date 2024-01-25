
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenFloat : Tween
    {
        public float start;
        public float end;
        public float lerped { get; protected private set; }

        public readonly CustomEvent<float> OnTweenFloat = new CustomEvent<float>();

        protected override void SetEssentials()
        {
            tweenType = TweenType.Float;
        }

        public TweenFloat()
        {
            SetEssentials();
        }

        public TweenFloat(int id)
        {
            SetEssentials();
            SetID(id);
        }

        public TweenFloat(float end)
        {
            SetEssentials();
            this.end = end;
        }

        public TweenFloat(float start, float end)
        {
            SetEssentials();
            this.start = start;
            this.end = end;
        }



        public TweenFloat SetStart(float start)
        {
            this.start = start;
            return this;
        }

        public TweenFloat SetEnd(float end)
        {
            this.end = end;
            return this;
        }

        public TweenFloat SetStartEnd(float start, float end)
        {
            this.start = start;
            this.end = end;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Mathf.LerpUnclamped(start, end, Ease(percent));
            OnTweenFloat?.Invoke(lerped);
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenFloat?.Invoke(lerped);
        }

        protected override void Loop()
        {
            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }

        protected override void PingPong()
        {
            float temp = start;
            start = end;
            end = temp;

            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }
    }
}