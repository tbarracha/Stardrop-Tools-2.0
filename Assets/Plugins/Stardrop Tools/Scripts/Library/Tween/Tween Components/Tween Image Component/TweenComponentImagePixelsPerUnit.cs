
namespace StardropTools.Tween.Components
{
    public class TweenComponentImagePixelsPerUnit : TweenComponentImage<float>
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenImagePixelsPerUnit(startValue, endValue, targets);
            else
                tween = new TweenImagePixelsPerUnit(endValue, targets);

            SetTweenEssentials();
        }
    }
}