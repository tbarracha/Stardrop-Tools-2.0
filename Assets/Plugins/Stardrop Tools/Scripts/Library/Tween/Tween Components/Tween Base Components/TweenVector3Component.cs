
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector3Component : TweenComponent
    {
        [Header("Vector3 Values")]
        public Vector3 startVector;
        public Vector3 endVector;

        public CustomEvent<Vector3> OnTweenColor => tween.asVector3.OnTweenVector3;

        public void SetStart(Vector3 start) => startVector = start;
        public void SetEnd(Vector3 end) => endVector = end;

        public void SetStartEnd(Vector3 start, Vector3 end)
        {
            startVector = start;
            endVector = end;
        }

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenVector3(startVector, endVector);
            else
                tween = new TweenVector3(endVector);

            SetTweenEssentials();

            tween.asVector3.OnTweenVector3.AddListener(SetTweenVector);
            tween.Play();

            return tween;
        }

        protected virtual void SetTweenVector(Vector3 vector) { }
    }
}