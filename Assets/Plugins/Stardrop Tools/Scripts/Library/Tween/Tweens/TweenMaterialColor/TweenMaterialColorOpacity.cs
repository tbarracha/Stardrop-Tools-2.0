
using UnityEngine;

namespace StardropTools.Tween
{
    public class TweenMaterialColorOpacity : TweenColorOpacity
    {
        public Material material;

        public TweenMaterialColorOpacity(Material material, float end) : base(material.color, end)
        {
            color = material.color;
        }

        public TweenMaterialColorOpacity(Material material, float start, float end) : base(material.color, start, end)
        {
            color = material.color;
        }

        public void SetMaterial(Material material)
        {
            this.material = material;
            color = material.color;
        }

        protected override void TweenUpdate(float percent)
        {
            base.TweenUpdate(percent);
            material.color = color;
        }
    }
}