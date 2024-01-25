
using UnityEngine;

namespace StardropTools.Grid
{
    [System.Serializable]
    public class PointGridRenderer
    {
        [Header("Points")]
        [SerializeField] Color pointColor   = Color.yellow;
        [SerializeField] float pointRadius  = .2f;
        [SerializeField] bool renderPoints  = true;

        [Header("Helpers")]
        [SerializeField] Color cornerColor  = Color.red;
        [SerializeField] float cornerRadius = .3f;
        [SerializeField] bool renderCorners = false;

        public bool     RenderPoints    => renderPoints;
        public bool     RenderCorners   => renderCorners;

        public Color    PointColor      => pointColor;
        public Color    CornerColor     => cornerColor;

        public float    CornerRadius    => cornerRadius;
        public float    PointRadius      => pointRadius;
    }
}