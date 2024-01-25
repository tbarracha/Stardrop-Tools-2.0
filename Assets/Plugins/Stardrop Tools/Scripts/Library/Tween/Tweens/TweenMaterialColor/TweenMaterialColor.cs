
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenMaterialColor : TweenColor
    {
        public Material material;

        public TweenMaterialColor(Material material, Color end) : base(end)
        {
            start = material.color;
        }

        public TweenMaterialColor(Material material, Color start, Color end) : base(start, end)
        {
            material.color = start;
        }

        public void SetMaterial(Material material)
        {
            this.material = material;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            material.color = lerped;
        }
    }
}