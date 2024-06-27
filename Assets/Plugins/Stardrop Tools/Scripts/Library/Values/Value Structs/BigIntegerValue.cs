using System;
using UnityEngine;
using System.Numerics;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct BigIntegerValue : IValue<BigInteger>
    {
        public enum BigIntAbbreviation
        {
            K,      // Thousand
            M,      // Million
            B,      // Billion
            T,      // Trillion
            Qa,     // Quadrillion
            Qi,     // Quintillion
            SI,     // Sextillion
            SP,     // Septillion
            Oc,     // Octillion
            N,      // Nonillion
            DC      // Decillion+
        }

        private BigInteger value;

        private readonly EventCallback<BigInteger> onValueChanged;
        public EventCallback<BigInteger> OnValueChanged => onValueChanged;

        public BigInteger Value
        {
            get => value;
            set => SetValue(value);
        }

        public BigIntegerValue(BigInteger value)
        {
            this.value = value;
            this.onValueChanged = new EventCallback<BigInteger>();
        }

        public BigIntegerValue(int value)
        {
            this.value = new BigInteger(value);
            this.onValueChanged = new EventCallback<BigInteger>();
        }

        public BigIntegerValue(long value)
        {
            this.value = new BigInteger(value);
            this.onValueChanged = new EventCallback<BigInteger>();
        }

        public BigIntegerValue(float value)
        {
            this.value = new BigInteger(value);
            this.onValueChanged = new EventCallback<BigInteger>();
        }

        public BigIntegerValue(string value)
        {
            if (BigInteger.TryParse(value, out var result))
            {
                this.value = result;
            }
            else
            {
                UnityEngine.Debug.LogError("Invalid BigInteger string input.");
                this.value = BigInteger.Zero;
            }

            this.onValueChanged = new EventCallback<BigInteger>();
        }

        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(value);
        }

        public BigInteger GetValue()
        {
            return value;
        }

        public void SetValue(BigInteger value, bool invoke = true)
        {
            this.value = value;
            InvokeEvents(invoke);
        }

        public void SetValue(int value, bool invoke = true)
        {
            this.value = new BigInteger(value);
            InvokeEvents(invoke);
        }

        public void SetValue(long value, bool invoke = true)
        {
            this.value = new BigInteger(value);
            InvokeEvents(invoke);
        }

        public void SetValue(float value, bool invoke = true)
        {
            this.value = new BigInteger(value);
            InvokeEvents(invoke);
        }

        public void SetValue(string value, bool invoke = true)
        {
            if (BigInteger.TryParse(value, out var result))
            {
                this.value = result;
            }
            else
            {
                UnityEngine.Debug.LogError("Invalid BigInteger string input.");
            }
            InvokeEvents(invoke);
        }



        // Mathematical Operations
        // ------------------------------------------------------------------------------

        public BigIntegerValue Add(BigIntegerValue other, bool invokeEvents = true)
        {
            value += other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public BigIntegerValue Subtract(BigIntegerValue other, bool invokeEvents = true)
        {
            value -= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public BigIntegerValue Multiply(BigIntegerValue other, bool invokeEvents = true)
        {
            value *= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }

        public BigIntegerValue Divide(BigIntegerValue other, bool invokeEvents = true)
        {
            if (other.Value == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return this;
            }

            value /= other.Value;
            InvokeEvents(invokeEvents);
            return this;
        }



        // Comparison Operators
        // ------------------------------------------------------------------------------

        public bool IsGreater(BigIntegerValue other)
        {
            return value > other.value;
        }

        public bool IsLess(BigIntegerValue other)
        {
            return value < other.value;
        }

        public bool IsGreaterOrEqual(BigIntegerValue other)
        {
            return value >= other.value;
        }

        public bool IsLessOrEqual(BigIntegerValue other)
        {
            return value <= other.value;
        }

        public override bool Equals(object obj)
        {
            return obj is BigIntegerValue bigIntValue &&
                   value == bigIntValue.value;
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static string AbbreviateBigInteger(BigInteger number)
        {
            if (number == 0)
            {
                return "0";
            }

            if (number < 1000)
            {
                return number.ToString();
            }

            BigInteger divisor;
            BigIntAbbreviation unitAbbreviation;

            if (number < BigInteger.Pow(10, 6))
            {
                divisor = 1000;
                unitAbbreviation = BigIntAbbreviation.K;
            }
            else if (number < BigInteger.Pow(10, 9))
            {
                divisor = BigInteger.Pow(10, 6);
                unitAbbreviation = BigIntAbbreviation.M;
            }
            else if (number < BigInteger.Pow(10, 12))
            {
                divisor = BigInteger.Pow(10, 9);
                unitAbbreviation = BigIntAbbreviation.B;
            }
            else if (number < BigInteger.Pow(10, 15))
            {
                divisor = BigInteger.Pow(10, 12);
                unitAbbreviation = BigIntAbbreviation.T;
            }
            else if (number < BigInteger.Pow(10, 18))
            {
                divisor = BigInteger.Pow(10, 15);
                unitAbbreviation = BigIntAbbreviation.Qa;
            }
            else if (number < BigInteger.Pow(10, 21))
            {
                divisor = BigInteger.Pow(10, 18);
                unitAbbreviation = BigIntAbbreviation.Qi;
            }
            else if (number < BigInteger.Pow(10, 24))
            {
                divisor = BigInteger.Pow(10, 21);
                unitAbbreviation = BigIntAbbreviation.SI;
            }
            else if (number < BigInteger.Pow(10, 27))
            {
                divisor = BigInteger.Pow(10, 24);
                unitAbbreviation = BigIntAbbreviation.SP;
            }
            else if (number < BigInteger.Pow(10, 30))
            {
                divisor = BigInteger.Pow(10, 27);
                unitAbbreviation = BigIntAbbreviation.Oc;
            }
            else if (number < BigInteger.Pow(10, 33))
            {
                divisor = BigInteger.Pow(10, 30);
                unitAbbreviation = BigIntAbbreviation.N;
            }
            else
            {
                divisor = BigInteger.Pow(10, 33);
                unitAbbreviation = BigIntAbbreviation.DC;
            }

            return string.Format("{0:0.##}{1}", (double)number / (double)divisor, unitAbbreviation);
        }

        public static BigIntegerValue FromJson(string json)
        {
            BigIntegerValue result = new BigIntegerValue();
            try
            {
                var data = JsonUtility.FromJson<BigIntegerValue>(json);
                result.SetValue(data.value);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }



        // Operator Overloads
        // ------------------------------------------------------------------------------

        public static BigIntegerValue operator +(BigIntegerValue a, BigIntegerValue b)
        {
            return new BigIntegerValue(a.Value + b.Value);
        }

        public static BigIntegerValue operator -(BigIntegerValue a, BigIntegerValue b)
        {
            return new BigIntegerValue(a.Value - b.Value);
        }

        public static BigIntegerValue operator *(BigIntegerValue a, BigIntegerValue b)
        {
            return new BigIntegerValue(a.Value * b.Value);
        }

        public static BigIntegerValue operator /(BigIntegerValue a, BigIntegerValue b)
        {
            if (b.Value == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            return new BigIntegerValue(a.Value / b.Value);
        }

        public static bool operator ==(BigIntegerValue a, BigIntegerValue b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(BigIntegerValue a, BigIntegerValue b)
        {
            return a.Value != b.Value;
        }

        public static bool operator >(BigIntegerValue a, BigIntegerValue b)
        {
            return a.Value > b.Value;
        }

        public static bool operator <(BigIntegerValue a, BigIntegerValue b)
        {
            return a.Value < b.Value;
        }

        public static bool operator >=(BigIntegerValue a, BigIntegerValue b)
        {
            return a.Value >= b.Value;
        }

        public static bool operator <=(BigIntegerValue a, BigIntegerValue b)
        {
            return a.Value <= b.Value;
        }
    }
}
