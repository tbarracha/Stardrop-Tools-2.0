
using UnityEngine;
using UnityEngine.UI;

namespace StardropTools.UI
{
    [RequireComponent(typeof(Image), typeof(UIPointerEvent))]
    public class UIButton : BaseRectTransform
    {
        public int ButtonID;
        [SerializeField] protected Button button;
        [SerializeField] protected UIPointerEvent pointerEvent;
        [SerializeField] protected bool useButton = true;

        public Button Button => button;
        public bool Interactible { get => button.interactable; set => button.interactable = value; }

        public readonly CustomEvent OnClick         = new CustomEvent();
        public readonly CustomEvent<int> OnClickID  = new CustomEvent<int>();

        public override void Initialize()
        {
            base.Initialize();

            if (useButton)
                button.onClick.AddListener(OnPressed);
            else
                pointerEvent.OnPointerUpEvent.AddListener(OnPressed);
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
                button = GetComponent<UnityEngine.UI.Button>();

            if (button == null)
                button = GetComponentInParent<UnityEngine.UI.Button>();

            if (pointerEvent == null)
                pointerEvent = GetComponent<UIPointerEvent>();

            if (pointerEvent == null)
                pointerEvent = GetComponentInParent<UIPointerEvent>();
        }
    }
}