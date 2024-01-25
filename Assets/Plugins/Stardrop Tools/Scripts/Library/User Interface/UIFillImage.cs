
using StardropTools.Tween;
using UnityEngine;

namespace StardropTools.UI
{
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    public class UIFillImage : BaseComponent
    {
        [SerializeField] protected UnityEngine.UI.Image image;
        protected TweenFloat tweenFillValue;

        public float Percent => image.fillAmount;


        public readonly CustomEvent OnValueChanged = new CustomEvent();
        public readonly CustomEvent<float> OnFillValueChanged = new CustomEvent<float>();


        public override void Initialize()
        {
            base.Initialize();
            tweenFillValue = new TweenFloat();
        }



        public void SetFill(float value)
        {
            image.fillAmount = value;
            FillValueChanged(image.fillAmount);
        }

        public void SetFill(float value, bool stopSliderValue = true)
        {
            if (stopSliderValue)
                StopTweenSliderValue();

            image.fillAmount = value;
            FillValueChanged(image.fillAmount);
        }

        void FillValueChanged(float value)
        {
            OnValueChanged?.Invoke();
            OnFillValueChanged?.Invoke(value);
        }



        public void TweenSliderValue(float fromValue, float toValue, float duration, EaseType easeType = EaseType.Linear)
        {
            tweenFillValue.SetStartEnd(fromValue, toValue);
            tweenFillValue.SetDuration(duration);
            tweenFillValue.SetEaseType(easeType);
            tweenFillValue.Play();

            tweenFillValue.OnTweenFloat.AddListener(SetFill);
        }

        public void TweenSliderValue(float fromValue, float toValue, float duration, AnimationCurve animCurve)
        {
            tweenFillValue.SetAnimationCurve(animCurve);
            TweenSliderValue(fromValue, toValue, duration, EaseType.AnimationCurve);
        }

        public void StopTweenSliderValue() => tweenFillValue.Stop();



        protected override void OnValidate()
        {
            base.OnValidate();

            if (image == null)
                image = GetComponent<UnityEngine.UI.Image>();
        }
    }
}