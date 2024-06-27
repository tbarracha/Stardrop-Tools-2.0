
namespace StardropTools.Tween.Components
{
    public class TweenComponentLineColorOpacity : TweenComponentLine<float>
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenLineColorOpacity(target.startColor, startValue, endValue, targets);
            else
                tween = new TweenLineColorOpacity(target.startColor, endValue, targets);

            SetTweenEssentials();
        }
    }
}