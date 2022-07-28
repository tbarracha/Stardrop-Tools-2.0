
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector4 : Tween
    {
        protected Vector4 start;
        protected Vector4 end;
        protected Vector4 lerped;

        public TweenVector4()
        {
            tweenType = TweenType.Vector4;
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
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(TweenState.running);
        }

        protected override void PingPong()
        {
            Vector4 temp = start;
            start = end;
            end = temp;

            ResetRuntime();
            ChangeState(TweenState.running);
        }
    }
}