
namespace StardropTools.Tween.Components
{
    public class TweenComponentSpriteRendererColor : TweenComponentSpriteRenderer<UnityEngine.Color>
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenSpriteRendererColor(startValue, endValue, targets);
            else
                tween = new TweenSpriteRendererColor(endValue, targets);

            SetTweenEssentials();
        }
    }
}