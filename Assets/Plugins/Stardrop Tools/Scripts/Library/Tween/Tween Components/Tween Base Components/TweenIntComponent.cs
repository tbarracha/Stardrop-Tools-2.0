
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenIntComponent : TweenComponent
    {
        [Header("Int Values")]
        public int startValue;
        public int endValue;

        public CustomEvent<int> OnTweenInt => tween.asInt.OnTweenInt;

        public void SetStart(int start) => startValue = start;
        public void SetEnd(int end) => endValue = end;

        public void SetStartEnd(int start, int end)
        {
            startValue = start;
            endValue = end;
        }

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenInt(startValue, endValue);
            else
                tween = new TweenInt(endValue);

            SetTweenEssentials();

            tween.asInt.OnTweenInt.AddListener(SetTweenInt);
            tween.Play();

            return tween;
        }

        protected virtual void SetTweenInt(int value) { }
    }
}