using System.Collections;
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(Toggle))]
    public class UIToggleButton : UIButton
    {
        [Header("Toggle Button")]
        [SerializeField] protected bool debugToggleState;
        [Space]
        [SerializeField] protected bool initialToggle;
        [SerializeField] protected bool initialValidateToggle;
        [SerializeField] protected bool isActingLikeDefaultButton;
        [SerializeField] protected bool allowUntoggleSelf = true;
        [SerializeField] protected bool canToggle = true;
        [SerializeField] protected float toggleCooldown = 0.05f;
        [Space]
        [SerializeField] protected Toggle toggle;
        [SerializeField] protected Transform parentComponents;
        [SerializeField] protected UIToggleOtherToggles otherToggles;
        [SerializeField] protected UIToggleButtonComponent[] toggleComponents;
        protected Coroutine toggleCR;

        public bool Value                                           => toggle.Value;
        public bool IsActingLikeDefaultButton                       => isActingLikeDefaultButton;

        public EventCallback OnToggleDry                              => toggle.OnToggle;
        public EventCallback<bool> OnToggle                           => toggle.OnToggleValue;
        
        public readonly EventCallback OnToggleTrue                    = new EventCallback();
        public readonly EventCallback OnToggleFalse                   = new EventCallback();

        public readonly EventCallback<int> OnToggleIndex              = new EventCallback<int>();

        public readonly EventCallback<int> OnToggleTrueIndex          = new EventCallback<int>();
        public readonly EventCallback<int> OnToggleFalseIndex         = new EventCallback<int>();


        public override void Initialize()
        {
            base.Initialize();

            SetToggle(initialToggle, initialValidateToggle);

            if (toggleComponents.Exists())
                for (int i = 0; i < toggleComponents.Length; i++)
                    toggleComponents[i].Initialize();
        }

        protected override void OnPressed()
        {
            base.OnPressed();

            // is a Toggle
            if (isActingLikeDefaultButton == false)
            {
                Toggle();
            }

            // default button
            else
            {

            }
        }

        public void SetToggle(bool value, bool validateEvents = false)
        {
            toggle.SetToggle(value, validateEvents);
            RefreshToggleComponents();
        }

        public virtual void Toggle()
        {
            if (canToggle == false)
                return;

            if (Value == true && allowUntoggleSelf == false)
                return;

            toggle.ToggleValue();
            OnValueChanged();
        }

        public void Toggle(bool value, bool validate = true)
        {
            if (validate && value == Value)
                return;

            toggle.ToggleValue(value);
            OnValueChanged();
        }

        protected void RefreshToggleComponents()
        {
            for (int i = 0; i < toggleComponents.Length; i++)
                toggleComponents[i].SetToggle(Value);
        }


        protected void OnValueChanged()
        {
            StartToggleCooldown();

            if (Value == true)
                OnTrue();
            else
                OnFalse();

            OnToggled();
            RefreshToggleComponents();
            
            if (debugToggleState)
                Debug.Log(name + ", toggled: " + Value);
        }

        protected virtual void OnTrue()
        {
            OnToggleTrue?.Invoke();
            OnToggleTrueIndex?.Invoke(ButtonID);
        }

        protected virtual void OnFalse()
        {
            OnToggleFalse?.Invoke();
            OnToggleFalseIndex?.Invoke(ButtonID);
        }

        protected virtual void OnToggled()
        {

        }


        public void ShouldActLikeDefaultButton(bool isDefaultButton)
        {
            isActingLikeDefaultButton = isDefaultButton;
        }


        public void SetAllowToggle(bool allowToggle)
        {
            canToggle = allowToggle;
        }

        void StartToggleCooldown()
        {
            StopToggleCooldown();
            toggleCR = StartCoroutine(ToggleCooldownCR());
        }

        void StopToggleCooldown()
        {
            if (toggleCR != null)
                StopCoroutine(toggleCR);
        }

        IEnumerator ToggleCooldownCR()
        {
            SetAllowToggle(false);

            float t = 0;
            while (t != toggleCooldown)
            {
                t = Mathf.Min(t + Time.deltaTime, toggleCooldown);
                yield return null;
            }

            SetAllowToggle(true);
        }

        public void GetOtherToggles()
        {
            if (otherToggles == null)
                return;

            otherToggles.GetOtherToggles();
        }


        [NaughtyAttributes.Button("Get Parent Components")]
        void GetParentComponents()
        {
            var transforms = GetComponentsInChildren<RectTransform>();

            for (int i = 0; i < transforms.Length; i++)
            {
                Transform t = transforms[i];
                var toggleComponents = Utilities.GetComponentListInChildren<UIToggleButtonComponent>(t);

                if (toggleComponents.Exists())
                {
                    parentComponents = t;
                    break;
                }
            }
        }



        protected override void OnValidate()
        {
            base.OnValidate();

            if (toggle == null)
                toggle = GetComponent<Toggle>();

            if (parentComponents != null && Utilities.GetComponentListInChildren<UIToggleButtonComponent>(parentComponents) != null)
                toggleComponents = Utilities.GetComponentListInChildren<UIToggleButtonComponent>(parentComponents).ToArray();
        }
    }
}