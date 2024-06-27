
namespace StardropTools.Tween.Components
{
    public class TweenComponentRotation : TweenComponentTransformVector3
    {
        protected override void CreateTween()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
            {
                if (hasStart)
                    tween = new TweenEulerRotation(startValue, endValue, targets);
                else
                    tween = new TweenEulerRotation(endValue, targets);
            }

            if (simulationSpace == SimulationSpace.LocalSpace)
            {
                if (hasStart)
                    tween = new TweenLocalEulerRotation(startValue, endValue, targets);
                else
                    tween = new TweenLocalEulerRotation(endValue, targets);
            }

            SetTweenEssentials();
        }
    }
}