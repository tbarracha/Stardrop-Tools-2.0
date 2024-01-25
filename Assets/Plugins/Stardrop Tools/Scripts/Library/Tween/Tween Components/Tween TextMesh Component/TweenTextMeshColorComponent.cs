
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenTextMeshColorComponent : TweenTextMeshComponent
    {
        public Color startColor;
        public Color endColor;

        public override Tween Play()
        {
            if (targets.Length == 0)
            {
                print("No targets to tween!");
                return null;
            }

            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                Tween tempTween;

                if (hasStart)
                    tempTween = new TweenTextMeshColor(target, startColor, endColor);
                else
                    tempTween = new TweenTextMeshColor(target, endColor);

                tempTween.SetID(target.GetHashCode())
                         .SetDurationAndDelay(duration, delay)
                         .SetEaseType(easeType);

                if (easeType == EaseType.AnimationCurve)
                    tempTween.SetAnimationCurve(curve);

                tempTween.Play();

                if (i == 0)
                    tween = tempTween;
            }

            SetTweenEssentials();
            return tween;
        }

        [NaughtyAttributes.Button("Get Start Image Color")]
        private void GetStart()
        {
            startColor = targets[0].color;
        }
    }
}