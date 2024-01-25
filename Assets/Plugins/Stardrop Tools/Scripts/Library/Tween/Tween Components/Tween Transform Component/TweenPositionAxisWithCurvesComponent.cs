
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenPositionAxisWithCurvesComponent : TweenTransformComponent
    {
        public Vector3 startPosition;
        public Vector3 endPosition;

        public CurveAxis axisInfluence;

        public override Tween Play()
        {
            tween = new TweenFloat(0, 1);

            SetTweenEssentials();
            tween.SetID(this).Play();

            tween.asFloat.OnTweenFloat.AddListener(EvaluatePosition);

            return tween;
        }

        void EvaluatePosition(float time)
        {
            Vector3 targetPosition = axisInfluence.Evaluate(time);
            
            if (simulationSpace == SimulationSpace.WorldSpace)
                target.position = targetPosition;

            else if (simulationSpace == SimulationSpace.LocalSpace)
                target.localPosition = targetPosition;
        }

        [NaughtyAttributes.Button("Get Start Position")]
        private void GetStart()
        {
            if (simulationSpace == SimulationSpace.WorldSpace)
                startPosition = target.position;
            else
                startPosition = target.localPosition;
        }
    }

    [System.Serializable]
    public class CurveAxis
    {
        [SerializeField] AnimationCurve curveX;
        [SerializeField] AnimationCurve curveY;
        [SerializeField] AnimationCurve curveZ;
        Vector3 evaluatedVector = Vector3.zero;

        public CurveAxis(AnimationCurve curveX, AnimationCurve curveY, AnimationCurve curveZ)
        {
            this.curveX = curveX;
            this.curveY = curveY;
            this.curveZ = curveZ;

            evaluatedVector = Vector3.zero;
        }

        public Vector3 Evaluate(float time)
        {
            evaluatedVector.x = curveX.Evaluate(time);
            evaluatedVector.y = curveY.Evaluate(time);
            evaluatedVector.z = curveZ.Evaluate(time);

            return evaluatedVector;
        }
    }
}