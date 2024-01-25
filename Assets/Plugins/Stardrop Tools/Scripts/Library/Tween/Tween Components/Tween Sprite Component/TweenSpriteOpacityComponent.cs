
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenSpriteOpacityComponent : TweenSpriteComponent
    {
        [Tooltip("0 = transparent, 1 = opaque")]
        [Range(0, 1)] public float startOpacity;
        [Tooltip("0 = transparent, 1 = opaque")]
        [Range(0, 1)] public float endOpacity;

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenSpriteRendererOpacity(target, startOpacity, endOpacity);
            else
                tween = new TweenSpriteRendererOpacity(target, endOpacity);

            SetTweenEssentials();
            tween.SetID(target).Play();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Opacity")]
        private void GetStart()
        {
            startOpacity = target.color.a;
        }
    }
}