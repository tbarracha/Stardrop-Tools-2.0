
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenRectSizeComponent : TweenRectTransformComponent
    {
        public Vector2 startRectSize;
        public Vector2 endRectSize;

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenRectSize(target, startRectSize, endRectSize);
            else
                tween = new TweenRectSize(target, endRectSize);

            SetTweenEssentials();
            tween.SetID(this).Play();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Rect Size")]
        private void GetStart()
        {
            startRectSize = target.rect.size;
        }
    }
}