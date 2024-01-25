
using UnityEngine;

namespace StardropTools.UI
{
    public abstract class UIToggleButtonComponent : BaseComponent
    {
        [SerializeField] protected UIToggleButton targetToggleButton;

        public abstract void SetToggle(bool value);



        [NaughtyAttributes.Button("Get Toggle Button")]
        protected void GetButton()
        {
            targetToggleButton = GetComponent<UIToggleButton>();

            if (targetToggleButton == null)
                targetToggleButton = GetComponentInParent<UIToggleButton>();
        }
    }
}