
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenAnchoredPositionAxisWithCurvesComponent : TweenRectTransformComponent
    {
        public Vector2 startPos;
        public Vector2 endPos;

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
            Vector2 lerped = Vector2.Lerp(startPos, endPos, time);
            Vector2 evaludated = axisInfluence.Evaluate(time);
            target.anchoredPosition = new Vector2(lerped.x * evaludated.x, lerped.y * evaludated.y);
        }

        [NaughtyAttributes.Button("Get Start Anchored Position")]
        private void GetStart()
        {
            startPos = target.anchoredPosition;
        }
    }
}