
using UnityEngine;
using NaughtyAttributes;

namespace StardropTools.Values
{
    public class ValueContainerInt : MonoBehaviour
    {
        [ProgressBar("Value Percent", 1, EColor.Gray)]
        [SerializeField] float percent;
        [SerializeField] int startValue;
        [SerializeField] int maxValue;
        [SerializeField] int value;
        [SerializeField] bool isEmpty;

        public int Value => value;
        public float PercentValue => percent;
        public bool IsEmpty => isEmpty;
        

        public readonly EventCallback<int> OnDecremented = new EventCallback<int>();
        public readonly EventCallback<int> OnIncremented = new EventCallback<int>();

        public readonly EventCallback<int> OnValueChanged = new EventCallback<int>();
        public readonly EventCallback<float> OnPercentChanged = new EventCallback<float>();
        public readonly EventCallback OnValueEmpty = new EventCallback();


        #region Constructors

        public void SetValue(int startHealth, int maxHealth)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            value = startHealth;

            CalculatePercent();
            OnValueChanged?.Invoke(value);
        }

        public void SetValue(int startHealth, int maxHealth, int health)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            this.value = health;

            CalculatePercent();
            OnValueChanged?.Invoke(value);
        }

        #endregion // Constructos

        float CalculatePercent()
        {
            percent = Mathf.Clamp(value / maxValue, 0, 1);
            OnPercentChanged?.Invoke(percent);

            return percent;
        }



        public int IncrementValue(int amount)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value + amount, 0, maxValue);

            if (value > 0 && isEmpty == true)
                isEmpty = false;

            CalculatePercent();
            OnValueChanged?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Value from 0 to 1
        /// </summary>
        public int IncrementPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int heal = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);

            return IncrementValue(heal);
        }



        public int DecrementValue(int amount)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value - amount, 0, maxValue);

            if (value == 0 && isEmpty == false)
            {
                isEmpty = true;
                OnValueEmpty?.Invoke();
            }

            CalculatePercent();
            OnValueChanged?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Value from 0 to 1
        /// </summary>
        public int DecrementPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int damage = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            CalculatePercent();

            return DecrementValue(damage);
        }



        public void ResetValue()
        {
            isEmpty = false;
            value = maxValue;

            CalculatePercent();
            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(int targetValue)
        {
            isEmpty = false;
            value = targetValue;

            CalculatePercent();
            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(float targetPercent)
        {
            isEmpty = false;
            value = Mathf.CeilToInt(targetPercent * maxValue);

            CalculatePercent();
            OnValueChanged?.Invoke(value);
        }
    }
}