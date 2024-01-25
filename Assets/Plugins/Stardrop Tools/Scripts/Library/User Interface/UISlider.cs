
using StardropTools.Tween;
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Slider))]
    public class UISlider : BaseComponent
    {
        [SerializeField] protected bool validateAnyChange          = true;
        [SerializeField] protected bool validateValueChange     = true;
        [SerializeField] protected bool validateWholeNumbers    = false;
        [SerializeField] protected UnityEngine.UI.Slider        slider;
        [SerializeField] protected TweenImageColorComponent[]   tweenColorsEnabled;
        [SerializeField] protected TweenImageColorComponent[]   tweenColorsDisabled;
        [SerializeField] protected TMPro.TextMeshProUGUI        textMesh;

        protected TweenFloat tweenSliderValue;

        public float Value          => slider.value;
        public int ValueInt         => Mathf.CeilToInt(slider.value);
        public bool IsInteractive   => slider.interactable;

        public bool ValidateAnyChange           => validateAnyChange;
        public bool ValidateSliderChange        => validateValueChange;
        public bool ValidateWholeNumbersChange  => validateWholeNumbers;

        public readonly CustomEvent         OnValueChangedDry       = new CustomEvent();
        public readonly CustomEvent<float>  OnValueChanged          = new CustomEvent<float>();
        public readonly CustomEvent<int>    OnWholeValueChanged     = new CustomEvent<int>();

        public override void Initialize()
        {
            base.Initialize();
            RefreshText();

            tweenSliderValue = new TweenFloat();
            slider.onValueChanged.AddListener(SliderValueChanged);
        }

        void SliderValueChanged(float value)
        {
            RefreshText();

            if (validateAnyChange == false)
                return;

            if (validateValueChange)
            {
                OnValueChangedDry?.Invoke();
                OnValueChanged?.Invoke(value);
            }

            if (validateWholeNumbers)
            {
                int intValue = Mathf.CeilToInt(value);
                if (value == intValue)
                    OnWholeValueChanged?.Invoke(intValue);
            }
        }

        public void RefreshText()
        {
            if (textMesh != null)
                textMesh.text = Value.ToString();
        }



        public void SetInteractible(bool value)
        {
            if (IsInteractive == value)
                return;

            if (value == false)
            {
                TweenManager.StopTweenComponents(tweenColorsEnabled);
                TweenManager.PlayTweenComponents(tweenColorsDisabled);
            }

            else
            {
                TweenManager.StopTweenComponents(tweenColorsDisabled);
                TweenManager.PlayTweenComponents(tweenColorsEnabled);
            }

            slider.interactable = value;
        }

        public void SetValidateAnyChange(bool validate)
            => validateAnyChange = validate;

        public void SetValidateSliderChanged(bool validate)
            => validateValueChange = validate;

        public void SetValidateWholeNumbers(bool validate)
            => validateWholeNumbers = validate;


        public void SetValue(float value)
        {
            slider.value = value;
        }

        public void SetValue(float value, bool stopSliderValue = true)
        {
            if (stopSliderValue)
                StopTweenSliderValue();

            slider.value = value;
        }

        public void SetUseWholeNumber(bool value)       => slider.wholeNumbers  = value;

        public void SetCheckForWholeNumbers(bool value) => validateWholeNumbers = value;


        public void SetRandomValue()
        {
            SetValue(Random.Range(slider.minValue, slider.maxValue));
        }

        public void SetAsMinValue()
        {
            SetValue(slider.minValue);
        }

        public void SetAsMaxValue()
        {
            SetValue(slider.maxValue);
        }

        public void TrySetZero()
        {
            if (slider.minValue == 0)
                SetValue(0);
            else
                SetAsMinValue();
        }

        

        public void SetMinMax(float min, float max)
        {
            StopTweenSliderValue();

            slider.minValue = min;
            slider.maxValue = max;
        }

        public void SetMinMaxAndValue(float min, float max, float value, bool useIntValues = false)
        {
            StopTweenSliderValue();

            slider.minValue = min;
            slider.maxValue = max;
            slider.value = value;

            slider.wholeNumbers = useIntValues;
        }



        public TweenFloat TweenSliderValue(float fromValue, float toValue, float duration, bool validate = true, EaseType easeType = EaseType.Linear)
        {
            SetMinMaxAndValue(Mathf.Min(fromValue, toValue), Mathf.Max(fromValue, toValue), fromValue);
            StopTweenSliderValue();

            tweenSliderValue = new TweenFloat();
            tweenSliderValue.SetStartEnd(fromValue, toValue);
            tweenSliderValue.SetDuration(duration);
            tweenSliderValue.SetEaseType(easeType);
            tweenSliderValue.Play();

            tweenSliderValue.OnTweenFloat.AddListener(SetValue);
            return tweenSliderValue;
        }

        public TweenFloat TweenSliderValue(float fromValue, float toValue, float duration, AnimationCurve animCurve, bool validate = true)
        {
            tweenSliderValue.SetAnimationCurve(animCurve);
            return TweenSliderValue(fromValue, toValue, duration, validate, EaseType.AnimationCurve);
        }

        public TweenFloat TweenSliderValueTo(float value, float duration, EaseType easeType = EaseType.Linear)
        {
            StopTweenSliderValue();

            tweenSliderValue = new TweenFloat();
            tweenSliderValue.SetStartEnd(Value, value);
            tweenSliderValue.SetDuration(duration);
            tweenSliderValue.SetEaseType(easeType);
            tweenSliderValue.Play();

            tweenSliderValue.OnTweenFloat.AddListener(SetValue);
            return tweenSliderValue;
        }

        public void StopTweenSliderValue()
        {
            if (tweenSliderValue != null)
            {
                tweenSliderValue.Stop();
                tweenSliderValue = null;
            }
        }



        protected override void OnValidate()
        {
            base.OnValidate();

            if (slider == null)
                slider = GetComponent<UnityEngine.UI.Slider>();
        }
    }
}