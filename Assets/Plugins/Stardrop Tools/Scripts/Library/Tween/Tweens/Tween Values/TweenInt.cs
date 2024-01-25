
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenInt : Tween
    {
        public int start;
        public int end;
        public int lerped { get; protected private set; }

        public readonly CustomEvent<int> OnTweenInt = new CustomEvent<int>();

        protected override void SetEssentials()
        {
            tweenType = TweenType.Int;
        }

        public TweenInt()
        {
            SetEssentials();
        }

        public TweenInt(int end)
        {
            SetEssentials();
            this.end = end;
        }

        public TweenInt(int start, int end)
        {
            SetEssentials();
            this.start = start;
            this.end = end;
        }



        public TweenInt SetStart(int start)
        {
            this.start = start;
            return this;
        }

        public TweenInt SetEnd(int end)
        {
            this.end = end;
            return this;
        }

        public TweenInt SetStartEnd(int start, int end)
        {
            this.start = start;
            this.end = end;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = (int)Mathf.LerpUnclamped(start, end, Ease(percent));
            OnTweenInt?.Invoke(lerped);
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenInt?.Invoke(lerped);
        }

        protected override void Loop()
        {
            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }

        protected override void PingPong()
        {
            int temp = start;
            start = end;
            end = temp;

            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }
    }
}