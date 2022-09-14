
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenImagePixelsPerUnitMultiplier : TweenFloat
    {
        public Image image;

        protected override void SetEssentials()
        {
            //tweenID = image.GetInstanceID();
            tweenType = TweenType.ImagePixelsPerUnitMultiplier;
        }

        public TweenImagePixelsPerUnitMultiplier(Image image, float start, float end)
        {
            this.image = image;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenImagePixelsPerUnitMultiplier(Image image, float end)
        {
            this.image = image;
            start = image.pixelsPerUnitMultiplier;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            image.pixelsPerUnitMultiplier = lerped;
        }
    }
}