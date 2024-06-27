
namespace StardropTools.Tween.Components
{
    public class TweenComponentPosition : TweenComponentTransformVector3
    {
        protected override void CreateTween()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
            {
                if (hasStart)
                    tween = new TweenPosition(startValue, endValue, targets);
                else
                    tween = new TweenPosition(endValue, targets);
            }

            if (simulationSpace == SimulationSpace.LocalSpace)
            {
                if (hasStart)
                    tween = new TweenLocalPosition(startValue, endValue, targets);
                else
                    tween = new TweenLocalPosition(endValue, targets);
            }

            SetTweenEssentials();
        }
    }
}