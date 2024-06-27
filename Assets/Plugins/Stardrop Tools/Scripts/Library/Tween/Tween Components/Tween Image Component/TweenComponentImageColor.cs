
namespace StardropTools.Tween.Components
{
    public class TweenComponentImageColor : TweenComponentImage<UnityEngine.Color>
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenImageColor(startValue, endValue, targets);
            else
                tween = new TweenImageColor(endValue, targets);

            SetTweenEssentials();
        }
    }
}