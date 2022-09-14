
using UnityEngine;
using TMPro;

namespace StardropTools.Tween
{
    public class TweenTextMeshColor : TweenColor
    {
        public TextMeshProUGUI textMesh;

        protected override void SetEssentials()
        {
            //tweenID = image.GetInstanceID();
            tweenType = TweenType.TextMeshColor;
        }

        public TweenTextMeshColor(TextMeshProUGUI textMesh, Color start, Color end)
        {
            this.textMesh = textMesh;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenTextMeshColor(TextMeshProUGUI textMesh, Color end)
        {
            this.textMesh = textMesh;
            start = textMesh.color;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            textMesh.color = lerped;
        }
    }
}