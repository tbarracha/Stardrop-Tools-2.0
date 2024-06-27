
using UnityEngine;

namespace StardropTools.Tween
{
    public interface ITweenable : IPlayableCallback<ITween>, IStoppable
    {
        public EaseType EaseType { get; set; }
        public LoopType LoopType { get; set; }
        public AnimationCurve EaseCurve { get; set; }

        public float Duration { get; set; }
        public float Delay { get; set; }
        public float TotalDuration { get; }

        public int LoopCount { get; set; }
        public bool IgnoreTimeScale { get; set; }
        public float Percent { get; }

        void Pause();
    }
}
