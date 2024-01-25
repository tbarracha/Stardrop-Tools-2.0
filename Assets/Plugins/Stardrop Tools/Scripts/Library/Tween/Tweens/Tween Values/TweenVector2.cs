
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector2 : Tween
    {
        public Vector2 start;
        public Vector2 end;
        public Vector2 lerped { get; protected private set; }

        public readonly CustomEvent<Vector2> OnTweenVector2 = new CustomEvent<Vector2>();

        protected override void SetEssentials()
        {
            tweenType = TweenType.Vector2;
        }

        public TweenVector2()
        {
            SetEssentials();
        }

        public TweenVector2(Vector2 end)
        {
            SetEssentials();
            this.end = end;
        }

        public TweenVector2(Vector2 start, Vector2 end)
        {
            SetEssentials();
            this.start = start;
            this.end = end;
        }



        public TweenVector2 SetStart(Vector2 start)
        {
            this.start = start;
            return this;
        }

        public TweenVector2 SetEnd(Vector2 end)
        {
            this.end = end;
            return this;
        }

        public TweenVector2 SetStartEnd(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Vector2.LerpUnclamped(start, end, Ease(percent));
            OnTweenVector2?.Invoke(lerped);
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenVector2?.Invoke(lerped);
        }

        protected override void Loop()
        {
            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }

        protected override void PingPong()
        {
            Vector2 temp = start;
            start = end;
            end = temp;

            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }
    }
}