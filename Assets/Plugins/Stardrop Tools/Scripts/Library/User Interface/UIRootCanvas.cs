using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools.UI
{
    public class UIRootCanvas : BaseRectTransform
    {
        [Header("Root Canvas")]
        [SerializeField] List<RectTransform> rectsToCopy;
        [SerializeField] bool getRects;
        [SerializeField] bool copy;

        public override void Initialize()
        {
            base.Initialize();

            CopySizes();
        }

        public void AddRectToCopy(RectTransform rectTransform, bool refreshCopy = false)
        {
            if (rectsToCopy.Contains(rectTransform) == false)
                rectsToCopy.Add(rectTransform);

            if (refreshCopy)
                CopySizes();
        }

        public void RemoveRectToCopy(RectTransform rectTransform)
        {
            if (rectsToCopy.Contains(rectTransform) == true)
                rectsToCopy.Remove(rectTransform);
        }

        public void CopySizes() => UIUtils.CopySizeDeltas(RectTransform, rectsToCopy.ToArray());



        protected override void OnValidate()
        {
            base.OnValidate();

            if (getRects)
            {
                rectsToCopy = Utilities.GetComponentListInChildren<RectTransform>(RectTransform);
                getRects = false;
            }

            if (copy)
            {
                CopySizes();
                copy = false;
            }
        }
    }
}