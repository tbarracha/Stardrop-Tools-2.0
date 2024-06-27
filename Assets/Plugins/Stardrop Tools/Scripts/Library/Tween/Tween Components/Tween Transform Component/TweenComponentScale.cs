namespace StardropTools.Tween.Components
{
    public class TweenComponentScale : TweenComponentTransformVector3
    {
        protected override void CreateTween()
        {
            if (hasStart)
                tween = new TweenLocalScale(startValue, endValue, targets);
            else
                tween = new TweenLocalScale(endValue, targets);

            SetTweenEssentials();
        }

        protected override void ValidateStartEndValues()
        {
            base.ValidateStartEndValues();

            if (simulationSpace != SimulationSpace.LocalSpace)
                simulationSpace = SimulationSpace.LocalSpace;
        }
    }
}