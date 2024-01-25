
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [RequireComponent(typeof(LineRenderer))]
    public class LineEffect : VisualEffect
    {
        [SerializeField] LineRenderer line;

        private List<Vector3> linePoints = new List<Vector3>();

        #region Play & Stop

        // TODO

        #endregion // Play & Stop


        #region Color

        public void SetColor(Color color)
        {
            line.startColor = color;
            line.endColor = color;
        }

        public void SetStartColor(Color color)
        {
            line.startColor = color;
        }

        public void SetEndColor(Color color)
        {
            line.endColor = color;
        }

        public void SetColorOpacity(float opacity)
        {
            Color currentColor = line.startColor;
            currentColor.a = opacity;
            line.startColor = currentColor;
            line.endColor = currentColor;
        }

        public void SetColorStartOpacity(float opacity)
        {
            Color currentColor = line.startColor;
            currentColor.a = opacity;
            line.startColor = currentColor;
        }

        public void SetColorEndOpacity(float opacity)
        {
            Color currentColor = line.endColor;
            currentColor.a = opacity;
            line.endColor = currentColor;
        }

        #endregion // Color


        #region Width

        public void SetWidth(float width)
        {
            line.startWidth = width;
            line.endWidth = width;
        }

        public void SetStartWidth(float width)
        {
            line.startWidth = width;
        }

        public void SetEndWidth(float width)
        {
            line.endWidth = width;
        }

        #endregion // Width


        #region Line Points

        public void SetUseWorldSpace(bool useWorldSpace)
        {
            line.useWorldSpace = useWorldSpace;
        }

        public void SetLinePoints(List<Vector3> points)
        {
            linePoints = new List<Vector3>(points);
            UpdateLinePoints();
        }

        public void SetLinePoints(params Vector3[] points)
        {
            linePoints = new List<Vector3>(points);
            UpdateLinePoints();
        }

        public void AddLinePoint(Vector3 point)
        {
            linePoints.Add(point);
            UpdateLinePoints();
        }

        public void RemoveLinePoint(Vector3 point)
        {
            linePoints.Remove(point);
            UpdateLinePoints();
        }

        private void UpdateLinePoints()
        {
            line.positionCount = linePoints.Count;
            line.SetPositions(linePoints.ToArray());
        }

        public List<Vector3> GetLinePoints()
        {
            return new List<Vector3>(linePoints);
        }

        #endregion // Line Points


        protected override void OnValidate()
        {
            base.OnValidate();

            if (line == null)
            {
                line = GetComponent<LineRenderer>();
            }
        }
    }
}