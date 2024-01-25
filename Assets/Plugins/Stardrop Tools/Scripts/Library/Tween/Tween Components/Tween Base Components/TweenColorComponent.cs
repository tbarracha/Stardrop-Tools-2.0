
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenColorComponent : TweenComponent
    {
        [Header("Color Values")]
        public Color startColor = Color.white;
        public Color endColor = Color.white;



        public Color TweenedColor => tween.asColor.lerped;

        public CustomEvent<Color> OnTweenColor => tween.asColor.OnTweenColor;

        public void SetStart(Color start) => startColor = start;
        public void SetEnd(Color end) => endColor = end;

        public void SetStartEnd(Color start, Color end)
        {
            startColor = start;
            endColor = end;
        }

        public override Tween Play()
        {
            if (hasStart)
                tween = new TweenColor(startColor, endColor);
            else
                tween = new TweenColor(endColor);

            SetTweenEssentials();

            tween.asColor.OnTweenColor.AddListener(SetTweenColor);
            tween.Play();

            return tween;
        }

        protected virtual void SetTweenColor(Color color) { }
    }
}