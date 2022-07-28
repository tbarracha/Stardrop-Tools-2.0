
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenQuaternion : Tween
    {
        protected Quaternion start;
        protected Quaternion end;
        protected Quaternion lerped;

        public TweenQuaternion()
        {
            tweenType = TweenType.Quaternion;
        }

        public TweenQuaternion SetStart(Quaternion start)
        {
            this.start = start;
            return this;
        }

        public TweenQuaternion SetEnd(Quaternion end)
        {
            this.end = end;
            return this;
        }

        public TweenQuaternion SetStartEnd(Quaternion start, Quaternion end)
        {
            this.start = start;
            this.end = end;
            return this;
        }

        protected override void TweenUpdate(float percent)
        {
            lerped = Quaternion.LerpUnclamped(start, end, Ease(percent));
        }

        protected override void Loop()
        {
            ResetRuntime();
            ChangeState(TweenState.running);
        }

        protected override void PingPong()
        {
            Quaternion temp = start;
            start = end;
            end = temp;

            ResetRuntime();
            ChangeState(TweenState.running);
        }
    }
}