﻿
using UnityEngine;

namespace StardropTools.Tween
{
    public abstract class TweenImageComponent : TweenComponent
    {
        [Header("Target Image")]
        [SerializeField] protected UnityEngine.UI.Image[] targets;

        protected virtual void SetTweenColor(Color color) { }
    }
}