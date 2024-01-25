
using UnityEngine;

namespace StardropTools.UI
{
    public class BaseRectTransform : BaseTransform
    {
        [SerializeField] RectTransform thisRectTransform;
        public RectTransform RectTransform => thisRectTransform;


        #region Rect & RectTransform

        public Vector2 AnchoredPosition { get => thisRectTransform.anchoredPosition;    set => thisRectTransform.anchoredPosition = value; }
        public Vector2 SizeDelta        { get => thisRectTransform.sizeDelta;           set => thisRectTransform.sizeDelta = value; }

        public float WidthDelta         { get => SizeDelta.x; set => SetWidthDelta(value); }
        public float HeightDelta        { get => SizeDelta.y; set => SetHeightDelta(value); }


        public Rect Rect                { get => thisRectTransform.rect; }
        public Vector2 SizeRect
        {
            get => thisRectTransform.rect.size;
            set
            {
                Rect rect = thisRectTransform.rect;
                rect.size = value;
            }
        }

        public float WidthRect  { get => SizeRect.x; set => SetWidthRect(value); }
        public float HeightRect { get => SizeRect.x; set => SetHeightRect(value); }


        public void SetWidthDelta(float width) => SizeDelta = VecUtils.SetVectorX(SizeDelta, width);
        public void SetHeightDelta(float height) => SizeDelta = VecUtils.SetVectorY(SizeDelta, height);

        public void SetWidthRect(float width) => SizeRect = VecUtils.SetVectorX(SizeRect, width);
        public void SetHeightRect(float height) => SizeRect = VecUtils.SetVectorY(SizeRect, height);
        #endregion // Rect


        protected override void OnValidate()
        {
            base.OnValidate();

            if (thisRectTransform == null)
                thisRectTransform = GetComponent<RectTransform>();
        }

        public void SetAnchoredPosition(Vector2 anchoredPosition) => thisRectTransform.anchoredPosition = anchoredPosition;
        public void SetAnchoredPosition(float x, float y) => thisRectTransform.anchoredPosition = new Vector2(x, y);

        // Set Pivot & Anchor
        public void SetPivot(UIPivot uiPivot) => UIUtils.SetRectTransformPivot(RectTransform, uiPivot);
        public void SetAnchor(UIAnchor uiAnchor) => UIUtils.SetRectTransformAnchor(RectTransform, uiAnchor);


        // Copy Sizes
        public void CopySizeRect(RectTransform rectTransform) => SizeRect = rectTransform.rect.size;
        public void CopySizeDelta(RectTransform rectTransform) => SizeDelta = rectTransform.sizeDelta;
    }
}