
using UnityEngine;

namespace StardropTools
{
    public class ValueContainerInt : MonoBehaviour
    {
        [SerializeField] int startValue;
        [SerializeField] int maxValue;
        [SerializeField] int value;
        [SerializeField] float percent;
        [SerializeField] bool isEmpty;

        public int Value => value;
        public float PercentValue => percent;
        public bool IsEmpty => isEmpty;
        

        public BaseEvent<int> OnRemoved = new BaseEvent<int>();
        public BaseEvent<int> OnAdded = new BaseEvent<int>();

        public BaseEvent<int> OnValueChanged = new BaseEvent<int>();
        public BaseEvent OnValueEmpty = new BaseEvent();


        #region Constructors

        public ValueContainerInt(int startHealth, int maxHealth)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            value = startHealth;
        }

        public ValueContainerInt(int startHealth, int maxHealth, int health)
        {
            this.startValue = startHealth;
            this.maxValue = maxHealth;
            this.value = health;
        }

        #endregion // Constructos


        public int RemoveValue(int amountToRemove)
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
        public int RemovePercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int damage = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return RemoveValue(damage);
        }



        public int AddValue(int amountToAdd)
        {
            if (isEmpty)
                return 0;

            value = Mathf.Clamp(value + amountToAdd, 0, maxValue);

            if (value > 0 && isEmpty == true)
                isEmpty = false;

            OnValueChanged?.Invoke(value);
            return value;
        }

        public int AddPercentValue(float percent, bool fromMaxValue)
        {
            if (isEmpty)
                return 0;

            int heal = fromMaxValue ? Mathf.CeilToInt(percent * maxValue) : Mathf.CeilToInt(percent * value);
            return AddValue(heal);
        }



        public void ResetValue()
        {
            isEmpty = false;
            value = maxValue;

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