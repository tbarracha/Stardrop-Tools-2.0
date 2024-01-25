
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenImageOpacityComponent : TweenImageComponent
    {
        [Tooltip("0 = transparent, 1 = opaque")]
        [Range(0, 1)] public float startOpacity;
        [Tooltip("0 = transparent, 1 = opaque")]
        [Range(0, 1)] public float endOpacity;

        public override Tween Play()
        {
            for (int i = 0; i < targets.Length; i++)
            {
                var target = targets[i];
                Tween tempTween;

                if (hasStart)
                    tempTween = new TweenImageOpacity(target, startOpacity, endOpacity);
                else
                    tempTween = new TweenImageOpacity(target, endOpacity);

                if (i == 0)
                    tween = tempTween;
                SetTweenEssentials();

                tempTween.SetID(target.GetHashCode()).Play();
            }

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Opaicty")]
        private void GetStart()
        {
            startOpacity = targets[0].color.a;
        }
    }
}