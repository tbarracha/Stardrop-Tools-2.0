
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.Tween
{
    public class TweenGraphicsColor : TweenColorComponent
    {
        [Header("Graphics")]
        [SerializeField] Image[]            images;
        [SerializeField] SpriteRenderer []  spriteRenderers;
        [SerializeField] LineRenderer   []  lineRenderers;
        [SerializeField] TrailRenderer  []  trailRenderers;

        protected override void SetTweenColor(Color color)
        {
            Utilities.SetImageArrayColor(images, color);
            Utilities.SetSpriteRendererArrayColor(spriteRenderers, color);

            for (int i = 0; i < lineRenderers.Length; i++)
                Utilities.SetLineColor(lineRenderers[i], color);

            for (int i = 0; i < trailRenderers.Length; i++)
                Utilities.SetTrailColor(trailRenderers[i], color);
        }
    }
}