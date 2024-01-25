
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenQuaternion : Tween
    {
        protected Quaternion start;
        protected Quaternion end;
        protected Quaternion lerped { get; protected private set; }

        public readonly CustomEvent<Quaternion> OnTweenQuaternion = new CustomEvent<Quaternion>();

        protected override void SetEssentials()
        {
            tweenType = TweenType.Quaternion;
        }

        public TweenQuaternion()
        {
            SetEssentials();
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
            OnTweenQuaternion?.Invoke(lerped);
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenQuaternion?.Invoke(lerped);
        }

        protected override void Loop()
        {
            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }

        protected override void PingPong()
        {
            Quaternion temp = start;
            start = end;
            end = temp;

            LoopIncrement();
            ResetRuntime();
            ChangeState(TweenState.Running);
        }
    }
}