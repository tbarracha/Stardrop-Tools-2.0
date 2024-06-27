
namespace StardropTools.Tween.Components
{
    public class TweenComponentImageColorOpacity : TweenComponentImage<float>
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenImageColorOpacity(target.color, startValue, endValue, targets);
            else
                tween = new TweenImageColorOpacity(target.color, endValue, targets);

            SetTweenEssentials();
        }
    }
}