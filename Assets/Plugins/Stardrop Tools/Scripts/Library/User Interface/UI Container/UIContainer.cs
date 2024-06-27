
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI
{
    public class UIContainer : BaseRectTransform, IUIContainer
    {
        [SerializeField] protected bool isOpen = true;
        [SerializeField] protected bool validateIfOpen = true;
        [SerializeField] protected bool disableOnClose = true;

        [Header("Open & Close Buttons")]
        [SerializeField] protected UIToggleButton toggleOpenClose;
        [SerializeField] protected Button[] openButtons;
        [SerializeField] protected Button[] closeButtons;

        public bool IsOpen => isOpen;
        public bool ValidateIfOpen { get => validateIfOpen; set => validateIfOpen = value; }
        public bool DisableOnClose { get => disableOnClose; set => disableOnClose = value; }

        public override void Initialize()
        {
            base.Initialize();

            if (toggleOpenClose != null)
            {
                toggleOpenClose.Initialize();
                toggleOpenClose.OnToggle.Subscribe(ToggledOpenClose);
            }

            for (int i = 0; i < openButtons.Length; i++)
                openButtons[i].onClick.AddListener(Open);

            for (int i = 0; i < closeButtons.Length; i++)
                closeButtons[i].onClick.AddListener(Close);
        }

        protected virtual void ToggledOpenClose(bool value)
        {
            if (value)
                Open();
            else
                Close();
        }

        public void SetOpen(bool isOpen)
        {
            if (isOpen)
                Open();
            else
                Close();
        }

        public virtual void Open()
        {
            DefaulOpen();
        }

        public virtual void Close()
        {
            DefaultClose();
        }

        protected void DefaulOpen()
        {
            if (validateIfOpen && isOpen)
                return;

            isOpen = true;
            SetActive(true);

            //print("Default Opening!");
        }

        protected void DefaultClose()
        {
            if (validateIfOpen && !isOpen)
                return;

            isOpen = false;

            if (disableOnClose)
                SetActive(false);

            //print("Default Closing!");
        }
    }
}