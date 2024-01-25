
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenShakeScaleComponent : TweenShakeTransformComponent
    {
        public override Tween Play()
        {
            tween = new TweenShakeLocalScale(target, target.localScale);

            SetTweenEssentials();
            tween.Play();

            return tween;
        }
    }
}