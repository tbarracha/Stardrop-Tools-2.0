
using NaughtyAttributes;
using System.Numerics;
using UnityEngine;

namespace StardropTools.Scriptables
{
    [CreateAssetMenu(fileName = "Big Int Value", menuName = defaultMenuName + "Scriptable Big Int")]
    public class ScriptableBigInt : ScriptableValue
    {
        [Header("Value")]
        [SerializeField] BigInt value;

        [Header("Player Prefs")]
        [SerializeField] string userKey;

        public BigInt Value => value;
        public BigInteger BigInteger => value.BigInteger;
        public string SerializedValue => value.SerializedValue;
        public string AbbreviatedValue => value.AbbreviatedValue;

        public readonly CustomEvent<BigInt> OnValueChanged = new CustomEvent<BigInt>();

        public string Abbreviate() => value.Abbreviate();

        BigInt CurrencyChanged()
        {
            Abbreviate();
            OnValueChanged?.Invoke(value);

            return value;
        }



        public BigInt SetValue(int value, bool invokeEvents)
        {
            this.value = value;

            if (invokeEvents)
                return CurrencyChanged();
            else
                return this.value;
        }

        public BigInt SetValue(BigInt value, bool invokeEvents)
        {
            this.value = value;

            if (invokeEvents)
                return CurrencyChanged();
            else
                return this.value;
        }

        public BigInt SetValue(BigInteger value, bool invokeEvents)
        {
            this.value = value;

            if (invokeEvents)
                return CurrencyChanged();
            else
                return this.value;
        }

        public BigInt SetValue(string stringValue, bool invokeEvents)
        {
            value = new BigInt(stringValue);

            if (invokeEvents)
                return CurrencyChanged();
            else
                return value;
        }



        public BigInt Increment(int amount)
        {
            return Increment(new BigInt(amount));
        }

        public BigInt Increment(BigInt amount)
        {
            value += amount;
            return CurrencyChanged();
        }

        public BigInt Increment(BigInteger amount)
        {
            return Increment(new BigInt(amount));
        }



        public BigInt Decrement(int amount)
        {
            return Decrement(new BigInt(amount));
        }

        public BigInt Decrement(BigInt amount)
        {
            value -= amount;
            return CurrencyChanged();
        }

        public BigInt Decrement(BigInteger amount)
        {
            return Decrement(new BigInt(amount));
        }



        public BigInt Multiply(int multiplier)
        {
            return Multiply(new BigInt(multiplier));
        }

        public BigInt Multiply(BigInt multiplier)
        {
            value *= multiplier;
            return CurrencyChanged();
        }

        public BigInt Multiply(BigInteger multiplier)
        {
            return Multiply(new BigInt(multiplier));
        }



        public BigInt Divide(int divisor)
        {
            return Divide(new BigInt(divisor));
        }

        public BigInt Divide(BigInt divisor)
        {
            if (divisor != BigInt.Zero)
            {
                value /= divisor;
            }
            return CurrencyChanged();
        }

        public BigInt Divide(BigInteger divisor)
        {
            return Divide(new BigInt(divisor));
        }



        [Button("Save")]
        public override void Save()
        {
            Save(userKey);
        }

        public override void Save(string key)
        {
            if (IsValidKey(key, debugSave) == false)
                return;

            PlayerPrefs.SetString(key, value.SerializedValue);
        }



        [Button("Load")]
        public override void Load()
        {
            Load(userKey);
        }

        public override void Load(string key)
        {
            if (IsValidKey(key, debugLoad) == false)
                return;

            string savedValue = PlayerPrefs.GetString(key);
            string serializedBigInt = string.IsNullOrEmpty(savedValue) ? "0" : savedValue;
            value = new BigInt(serializedBigInt);
            Abbreviate();

            Debug.Log("Loaded Value: " + serializedBigInt);
        }



        [Button("Delete Key")]
        public override void DeleteKey()
        {
            PlayerPrefs.DeleteKey(userKey);
        }

        [Button("Reset Value")]
        public override void ResetValue()
        {
            value = new BigInt("00");
            Abbreviate();
        }



        private void OnValidate()
        {
            Abbreviate();
        }
    }
}
