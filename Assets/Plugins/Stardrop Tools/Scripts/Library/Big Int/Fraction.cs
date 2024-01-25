using System;
using UnityEngine;

namespace StardropTools
{
    [Serializable]
    public struct Fraction
    {
        [SerializeField] private int numerator;
        [SerializeField] private int denominator;

        public int Numerator => numerator;
        public int Denominator => denominator;

        public Fraction(int numerator, int denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public static Fraction ConvertFloatToFraction(float floatValue)
        {
            const float epsilon = 0.000000001f; // Adjust the epsilon based on your precision needs

            int denominator = 1;
            while (Math.Abs(floatValue - Math.Round(floatValue)) > epsilon)
            {
                floatValue *= 10;
                denominator *= 10;
            }

            int numerator = (int)Math.Round(floatValue);
            return new Fraction(numerator, denominator);
        }
    }
}
