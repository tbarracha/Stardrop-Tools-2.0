
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenFloatComponent : TweenComponent
    {
        [Header("Float Values")]
        public float startValue;
        public float endValue;

        public CustomEvent<float> OnTweenFloat => tween.asFloat.OnTweenFloat;

        public void SetStart(float start) => startValue = start;
        public void SetEnd(float end) => endValue = end;

        public void SetStartEnd(float start, float end)
        {
            startValue = start;
            endValue = end;
        }

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenFloat(startValue, endValue);
            else
                tween = new TweenFloat(endValue);

            SetTweenEssentials();

            tween.asFloat.OnTweenFloat.AddListener(SetTweenFloat);
            tween.Play();

            return tween;
        }

        protected virtual void SetTweenFloat(float value) { }
    }
}