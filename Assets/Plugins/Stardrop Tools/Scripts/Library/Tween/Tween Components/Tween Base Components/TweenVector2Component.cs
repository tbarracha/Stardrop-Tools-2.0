
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector2Component : TweenComponent
    {
        [Header("Vector2 Values")]
        public Vector2 startVector;
        public Vector2 endVector;

        public CustomEvent<Vector2> OnTweenColor => tween.asVector2.OnTweenVector2;

        public void SetStart(Vector2 start) => startVector = start;
        public void SetEnd(Vector2 end) => endVector = end;

        public void SetStartEnd(Vector2 start, Vector2 end)
        {
            startVector = start;
            endVector = end;
        }

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenVector2(startVector, endVector);
            else
                tween = new TweenVector2(endVector);

            SetTweenEssentials();

            tween.asVector2.OnTweenVector2.AddListener(SetTweenVector);
            tween.Play();

            return tween;
        }

        protected virtual void SetTweenVector(Vector2 vector) { }
    }
}