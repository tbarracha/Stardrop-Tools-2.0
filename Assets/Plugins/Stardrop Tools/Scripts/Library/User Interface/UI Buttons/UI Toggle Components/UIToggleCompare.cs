
using UnityEngine;

namespace StardropTools.UI
{
    public class UIToggleCompare : UIToggleButtonComponent
    {
        [SerializeField] protected Toggle toggleToCompare;
        [SerializeField] protected bool allowedValue;

        public override void Initialize()
        {
            base.Initialize();
            targetToggleButton.OnToggle.AddListener(SetToggle);
        }


        public override void SetToggle(bool value)
        {
            if (value == true && toggleToCompare.Value != allowedValue)
            {
                targetToggleButton.Toggle(false);
            }
        }
    }
}