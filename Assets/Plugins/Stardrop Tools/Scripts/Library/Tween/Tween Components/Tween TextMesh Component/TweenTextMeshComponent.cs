
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenTextMeshComponent : TweenComponent
    {
        [Header("Target Text Meshes")]
        [SerializeField] protected TMPro.TextMeshProUGUI[] targets;

        protected virtual void SetTweenColor(Color color) { }
    }
}