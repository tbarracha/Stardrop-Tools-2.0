
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenFloat : Tween
    {
        public float start;
        public float end;
        public float lerped;

        public TweenFloat()
        {
            tweenType = TweenType.Float;
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
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(TweenState.running);
        }

        protected override void PingPong()
        {
            float temp = start;
            start = end;
            end = temp;

            ResetRuntime();
            ChangeState(TweenState.running);
        }
    }
}