using System;
using System.Numerics;
using UnityEngine;

namespace StardropTools
{
    [Serializable]
    public class BigInt : ISerializationCallbackReceiver
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

        [SerializeField]
        private string serializedValue;
        [SerializeField]
        private string abbreviatedValue;

        public string SerializedValue => serializedValue;
        public string AbbreviatedValue => abbreviatedValue;
        public BigInt Value => value;
        public BigInteger BigInteger => value;

        public static BigInt Zero => BigInteger.Zero;

        public BigInt(BigInteger value)
        {
            this.value = value;
        }

        public BigInt(string value)
        {
            this.value = BigInteger.Parse(value);
        }

        public static implicit operator BigInt(int value)
        {
            return new BigInt(value);
        }

        public static implicit operator BigInt(long value)
        {
            return new BigInt(value);
        }

        public static implicit operator BigInt(BigInteger value)
        {
            return new BigInt(value);
        }

        public static implicit operator BigInteger(BigInt bigint)
        {
            return bigint.value;
        }

        public static bool operator >(BigInt left, BigInt right)
        {
            return left.value > right.value;
        }

        public static bool operator <(BigInt left, BigInt right)
        {
            return left.value < right.value;
        }

        public static bool operator >=(BigInt left, BigInt right)
        {
            return left.value >= right.value;
        }

        public static bool operator <=(BigInt left, BigInt right)
        {
            return left.value <= right.value;
        }

        public static bool operator ==(BigInt left, BigInt right)
        {
            return left.value == right.value;
        }

        public static bool operator !=(BigInt left, BigInt right)
        {
            return left.value != right.value;
        }

        public static BigInt operator +(BigInt left, BigInt right)
        {
            return new BigInt(left.value + right.value);
        }

        public static BigInt operator ++(BigInt bigint)
        {
            return new BigInt(bigint.value + 1);
        }

        public static BigInt operator -(BigInt left, BigInt right)
        {
            return new BigInt(left.value - right.value);
        }

        public static BigInt operator --(BigInt bigint)
        {
            return new BigInt(bigint.value - 1);
        }

        public static BigInt operator *(BigInt left, BigInt right)
        {
            return new BigInt(left.value * right.value);
        }

        public static BigInt operator /(BigInt dividend, BigInt divisor)
        {
            return new BigInt(dividend.value / divisor.value);
        }

        public bool IsHigher(BigInt other)
        {
            return this.value > other.value;
        }

        public bool IsHigher(BigInteger other)
        {
            return this.value > other;
        }

        public bool IsHigher(int other)
        {
            return this.value > other;
        }


        public override string ToString()
        {
            return value.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is BigInt @int &&
                   value.Equals(@int.value);
        }

        // Implement ISerializationCallbackReceiver interface
        public void OnBeforeSerialize()
        {
            SerializeValue();
        }

        public void OnAfterDeserialize()
        {
            DeserializeValue();
        }

        private void SerializeValue()
        {
            serializedValue = value.ToString();
        }

        private void DeserializeValue()
        {
            if (string.IsNullOrEmpty(serializedValue))
            {
                serializedValue = "0";
            }

            if (BigInteger.TryParse(serializedValue, out BigInteger result))
            {
                value = result;
            }
            else
            {
                // Handle the case where the deserialization fails
                Debug.LogError("Failed to deserialize BigInt value");
                value = new BigInteger(0);
            }

            Abbreviate();
        }

        public string Abbreviate()
        {
            abbreviatedValue = AbbreviateNumber(value);
            return abbreviatedValue;
        }

        public static string AbbreviateNumber(BigInteger number)
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
    }
}
