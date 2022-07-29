
using UnityEngine;

namespace StardropTools
{
    public class ValueContainerFloat : MonoBehaviour
    {
        [SerializeField] float startValue;
        [SerializeField] float maxValue;
        [SerializeField] float value;
        [SerializeField] float percent;
        [SerializeField] bool isEmpty;

        public float Value => value;
        public float PercentValue => percent;
        public bool IsEmpty => isEmpty;
        

        public BaseEvent<float> OnRemoved = new BaseEvent<float>();
        public BaseEvent<float> OnAdded = new BaseEvent<float>();

        public BaseEvent<float> OnValueChanged = new BaseEvent<float>();
        public BaseEvent OnValueEmpty = new BaseEvent();


        #region Constructors

        public ValueContainerFloat(float startHealth, float maxHealth)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            value = startHealth;
        }

        public ValueContainerFloat(float startHealth, float maxHealth, float health)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            this.value = health;
        }

        #endregion // Constructos


        public float RemoveValue(int amountToRemove)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value - amountToRemove, 0, maxValue);

            if (value == 0 && isEmpty == false)
            {
                isEmpty = true;
                OnValueEmpty?.Invoke();
            }

            OnValueChanged?.Invoke(value);
            return value;
        }

        /// <summary>
        /// Value from 0 to 1
        /// </summary>
        public float RemovePercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int damage = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return RemoveValue(damage);
        }



        public float AddValue(int amountToAdd)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value + amountToAdd, 0, maxValue);

            if (value > 0 && isEmpty == true)
                isEmpty = false;

            OnValueChanged?.Invoke(value);
            return value;
        }

        public float AddPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int heal = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return AddValue(heal);
        }



        public void ResetValue()
        {
            isEmpty = false;
            value = startValue;

            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(int resetValue)
        {
            isEmpty = false;
            value = resetValue;

            OnValueChanged?.Invoke(value);
        }

        public void ResetValue(float percentMaxValue)
        {
            isEmpty = false;
            value = Mathf.CeilToInt(percentMaxValue * maxValue);

            OnValueChanged?.Invoke(value);
        }
    }
}