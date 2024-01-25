
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector4 : Tween
    {
        protected Vector4 start;
        protected Vector4 end;
        protected Vector4 lerped { get; protected private set; }

        public readonly CustomEvent<Vector4> OnTweenVector4 = new CustomEvent<Vector4>();

        protected override void SetEssentials()
        {
            tweenType = TweenType.Vector4;
        }

        public TweenVector4()
        {
            SetEssentials();
        }

        public TweenVector4(Vector4 end)
        {
            SetEssentials();
            this.end = end;
        }

        public TweenVector4(Vector4 start, Vector4 end)
        {
            SetEssentials();
            this.start = start;
            this.end = end;
        }



        public TweenVector4 SetStart(Vector4 start)
        {
            this.start = start;
            return this;
        }

        public TweenVector4 SetEnd(Vector4 end)
        {
            this.end = end;
            return this;
        }

        public TweenVector4 SetStartEnd(Vector4 start, Vector4 end)
        {
            this.start = start;
            this.end = end;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector4.LerpUnclamped(start, end, Ease(percent));
            OnTweenVector4?.Invoke(lerped);
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenVector4?.Invoke(lerped);
        }

        protected override void Loop()
        {
            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }

        protected override void PingPong()
        {
            Vector4 temp = start;
            start = end;
            end = temp;

            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }
    }
}