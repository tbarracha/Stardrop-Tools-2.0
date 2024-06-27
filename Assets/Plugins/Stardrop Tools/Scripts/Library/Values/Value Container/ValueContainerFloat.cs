
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools.Values
{
    public class ValueContainerFloat : MonoBehaviour
    {
        [ProgressBar("Value Percent", 1, EColor.Gray)]
        [SerializeField] float percent;
        [SerializeField] float startValue;
        [SerializeField] float maxValue;
        [SerializeField] float value;
        [SerializeField] bool isEmpty;

        public float Value => value;
        public float PercentValue => percent;
        public bool IsEmpty => isEmpty;
        

        public readonly EventCallback<float> OnDecremented = new EventCallback<float>();
        public readonly EventCallback<float> OnIncremented = new EventCallback<float>();

        public readonly EventCallback<float> OnValueChanged = new EventCallback<float>();
        public readonly EventCallback<float> OnPercentChanged = new EventCallback<float>();
        public readonly EventCallback OnValueEmpty = new EventCallback();


        #region Setters

        public void SetValues(float startHealth, float maxHealth)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            value = startHealth;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        public void SetValues(float startHealth, float maxHealth, float health)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            this.value = health;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        #endregion // Setters

        float GetPercent()
        {
            percent = Mathf.Clamp(value / maxValue, 0, 1);
            OnPercentChanged?.Invoke(percent);

            return percent;
        }



        public float IncrementValue(int amount)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value + amount, 0, maxValue);

            if (value > 0 && isEmpty == true)
                isEmpty = false;

            GetPercent();
            OnValueChanged?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Value from 0 to 1
        /// </summary>
        public float IncrementPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int increment = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return IncrementValue(increment);
        }



        public float DecrementValue(int amount)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value - amount, 0, maxValue);

            if (value == 0 && isEmpty == false)
            {
                isEmpty = true;
                OnValueEmpty?.Invoke();
            }

            GetPercent();
            OnValueChanged?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Value from 0 to 1
        /// </summary>
        public float DecrementPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int decrement = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return DecrementValue(decrement);
        }



        public void ResetValue()
        {
            isEmpty = false;
            value = startValue;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(int targetValue)
        {
            isEmpty = false;
            value = targetValue;

            GetPercent();
            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(float targetPercent)
        {
            isEmpty = false;
            value = Mathf.CeilToInt(targetPercent * maxValue);

            GetPercent();
            OnValueChanged?.Invoke(value);
        }
    }
}