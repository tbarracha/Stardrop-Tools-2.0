
namespace StardropTools.Tween.Components
{
    public class TweenComponentSpriteRendererOpacity : TweenComponentSpriteRenderer<float>
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenSpriteRendererColorOpacity(target.color, startValue, endValue, targets);
            else
                tween = new TweenSpriteRendererColorOpacity(target.color, endValue, targets);

            SetTweenEssentials();
        }
    }
}