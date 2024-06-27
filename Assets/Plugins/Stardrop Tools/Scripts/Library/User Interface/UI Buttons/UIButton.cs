
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI
{
    [RequireComponent(typeof(Image))]
    public class UIButton : BaseRectTransform
    {
        public int ButtonID = -1;
        [SerializeField] protected Button button;
        [SerializeField] protected bool useButton = true;

        public Button Button => button;
        public bool Interactible { get => button.interactable; set => button.interactable = value; }

        public readonly EventCallback OnClick         = new EventCallback();
        public readonly EventCallback<int> OnClickID  = new EventCallback<int>();

        public override void Initialize()
        {
            base.Initialize();

            if (useButton)
                button.onClick.AddListener(OnPressed);
        }

        protected virtual void OnPressed()
        {
            OnClick?.Invoke();
            OnClickID?.Invoke(ButtonID);
        }

        public void SetInteractible(bool value)
        {
            button.interactable = value;
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            if (button == null)
                button = GetComponent<Button>();

            if (button == null)
                button = GetComponentInParent<Button>();
        }
    }
}