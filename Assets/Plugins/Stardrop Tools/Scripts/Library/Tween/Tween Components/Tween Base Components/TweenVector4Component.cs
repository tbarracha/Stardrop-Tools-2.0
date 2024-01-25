
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenVector4Component : TweenComponent
    {
        [Header("Vector4 Values")]
        public Vector4 startVector;
        public Vector4 endVector;

        public CustomEvent<Vector4> OnTweenColor => tween.asVector4.OnTweenVector4;

        public void SetStart(Vector4 start) => startVector = start;
        public void SetEnd(Vector4 end) => endVector = end;

        public void SetStartEnd(Vector4 start, Vector4 end)
        {
            startVector = start;
            endVector = end;
        }

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenVector4(startVector, endVector);
            else
                tween = new TweenVector4(endVector);

            SetTweenEssentials();

            tween.asVector4.OnTweenVector4.AddListener(SetTweenVector);
            tween.Play();

            return tween;
        }

        protected virtual void SetTweenVector(Vector4 vector) { }
    }
}