
namespace StardropTools.Tween.Components
{
    public abstract class TweenComponentTransformVector3 : TweenComponentTransform<UnityEngine.Vector3>
    {
#if UNITY_EDITOR
        protected override void ValidateStartEndValues()
        {
            if (uniformStart)
            {
                startValue.y = startValue.x;
                startValue.z = startValue.x;
            }

            if (uniformEnd)
            {
                endValue.y = endValue.x;
                endValue.z = endValue.x;
            }
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            ValidateStartEndValues();
        }
#endif
    }
}