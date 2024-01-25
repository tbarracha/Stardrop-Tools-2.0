
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenAnchoredPositionComponent : TweenRectTransformComponent
    {
        public Vector2 startPos;
        public Vector2 endPos;

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenAnchoredPosition(target, startPos, endPos);
            else
                tween = new TweenAnchoredPosition(target, endPos);

            SetTweenEssentials();
            tween.SetID(this).Play();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Anchored Position")]
        private void GetStart()
        {
            startPos = target.anchoredPosition;
        }
    }
}