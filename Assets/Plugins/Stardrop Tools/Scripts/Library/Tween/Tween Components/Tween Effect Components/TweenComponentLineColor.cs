
namespace StardropTools.Tween.Components
{
    public class TweenComponentLineColor : TweenComponentLine<UnityEngine.Color>
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenLineColor(startValue, endValue, targets);
            else
                tween = new TweenLineColor(endValue, targets);

            SetTweenEssentials();
        }
    }
}