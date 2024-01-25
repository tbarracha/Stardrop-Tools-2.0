
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenTextMeshOpacity : TweenColorOpacity
    {
        public TextMeshProUGUI textMesh;

        protected override void SetEssentials()
        {
            tweenType = TweenType.TextMeshOpacity;
        }

        public TweenTextMeshOpacity(TextMeshProUGUI textMesh, float start, float end)
            : base (textMesh.color, start, end)
        {
            this.textMesh = textMesh;
            this.start = start;
            this.end = end;

            SetEssentials();
        }

        public TweenTextMeshOpacity(TextMeshProUGUI textMesh, float end)
            : base (textMesh.color, end)
        {
            this.textMesh = textMesh;
            start = textMesh.color.a;
            this.end = end;

            SetEssentials();
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);

            if (textMesh == null)
                ChangeState(TweenState.Canceled);

            textMesh.color = color;
        }
    }
}