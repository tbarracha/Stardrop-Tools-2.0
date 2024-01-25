﻿
using UnityEngine;

namespace StardropTools.Tween
{
    /// <summary>
    /// 0 = transparent, 1 = opaque
    /// </summary>
    public class TweenColorOpacity : TweenFloat
    {
        public Color color;

        public readonly CustomEvent<Color> OnTweenColorOpacity = new CustomEvent<Color>();
             
        protected override void SetEssentials()
        {
            //tweenID = color.GetHashCode();
            tweenType = TweenType.ColorOpacity;
        }

        public TweenColorOpacity(Color color, float start, float end)
        {
            this.color = color;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenColorOpacity(Color color, float end)
        {
            this.color = color;
            start = color.a;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            color.a = lerped;

            OnTweenColorOpacity?.Invoke(color);
        }

        protected override void Complete()
        {
            base.Complete();
            OnTweenColorOpacity?.Invoke(color);
        }
    }
}