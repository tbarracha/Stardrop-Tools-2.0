
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenSizeDeltaComponent : TweenRectTransformComponent
    {
        public Vector2 startSizeDelta;
        public Vector2 endSizeDelta;
        public SizeFitMode fitMode;
        bool hasCalculatedFitMode = false;

        public override Tween Play()
        {
            if (fitMode != SizeFitMode.None && hasCalculatedFitMode == false)
            {
                float minWidth      = LayoutUtility.GetMinWidth(target);
                float minHeight     = LayoutUtility.GetMinHeight(target);
                float prefWidth     = LayoutUtility.GetPreferredWidth(target);
                float prefHeight    = LayoutUtility.GetPreferredHeight(target);

                switch (fitMode)
                {
                    case SizeFitMode.PrefSize:
                        endSizeDelta = new Vector2(prefWidth, prefHeight);
                        break;

                    case SizeFitMode.MinWidth:
                        endSizeDelta.x = minWidth;
                        break;

                    case SizeFitMode.MinHeight:
                        endSizeDelta.y = minHeight;
                        break;

                    case SizeFitMode.PrefWidth:
                        endSizeDelta.x = prefWidth;
                        break;

                    case SizeFitMode.PrefHeight:
                        endSizeDelta.y = prefHeight;
                        break;
                }

                hasCalculatedFitMode = true;
            }

            if (hasStart)
                tween = new TweenSizeDelta(target, startSizeDelta, endSizeDelta);
            else
                tween = new TweenSizeDelta(target, endSizeDelta);

            SetTweenEssentials();
            tween.SetID(target.GetHashCode()).Play();

            return tween;
        }

        [NaughtyAttributes.Button("Get Start Size Delta")]
        private void GetStart()
        {
            startSizeDelta = target.rect.size;
        }
    }
}