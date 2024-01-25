
using UnityEngine;

namespace StardropTools
{
    public class Toggle : MonoBehaviour
    {
        [SerializeField] bool toggle;

        public bool Value => toggle;

        public readonly CustomEvent OnToggle = new CustomEvent();
        public readonly CustomEvent<bool> OnToggleValue = new CustomEvent<bool>();

        public readonly CustomEvent OnToggleTrue = new CustomEvent();
        public readonly CustomEvent OnToggleFalse = new CustomEvent();

        public void SetToggle(bool value, bool invokeEvents)
        {
            toggle = value;

            if (invokeEvents)
                ToggleEvents();
        }

        public bool ToggleValue()
        {
            toggle = !toggle;
            ToggleEvents();

            return toggle;
        }

        public void ToggleValue(bool value)
        {
            if (value == Value)
                return;

            toggle = value;
            ToggleEvents();
        }

        void ToggleEvents()
        {
            OnToggle?.Invoke();
            OnToggleValue?.Invoke(toggle);

            if (toggle == true)
                OnToggleTrue?.Invoke();

            if (toggle == false)
                OnToggleFalse?.Invoke();
        }
    }
}