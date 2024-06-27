using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct ColorValue : IValue<Color>
    {
        private readonly float[] colorValues;

        private readonly EventCallback<Color> onValueChanged;
        public EventCallback<Color> OnValueChanged => onValueChanged;

        public Color Value
        {
            get => new Color(colorValues[0], colorValues[1], colorValues[2], colorValues[3]);
            set => SetValue(value);
        }

        public float r
        {
            get => colorValues[0];
            set => SetR(value);
        }

        public float g
        {
            get => colorValues[1];
            set => SetG(value);
        }

        public float b
        {
            get => colorValues[2];
            set => SetB(value);
        }

        /// <summary>
        /// Alpha component of the color (0 is transparent, 1 is opaque).
        /// </summary>
        public float a
        {
            get => colorValues[3];
            set => SetA(value);
        }

        // Constructors
        public ColorValue(Color value)
        {
            this.colorValues = new float[4] { value.r, value.g, value.b, value.a };
            this.onValueChanged = new EventCallback<Color>();
        }

        public ColorValue(ColorValue value)
        {
            this.colorValues = new float[4] { value.r, value.g, value.b, value.a };
            this.onValueChanged = new EventCallback<Color>();
        }

        public ColorValue(float r, float g, float b, float a)
        {
            this.colorValues = new float[4] { r, g, b, a };
            this.onValueChanged = new EventCallback<Color>();
        }

        public ColorValue(float r, float g, float b)
        {
            this.colorValues = new float[4] { r, g, b, 1 };
            this.onValueChanged = new EventCallback<Color>();
        }

        // Methods
        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(Value);
        }

        public Color GetValue()
        {
            return Value;
        }

        public void SetValue(Color newValue, bool invoke = true)
        {
            colorValues[0] = newValue.r;
            colorValues[1] = newValue.g;
            colorValues[2] = newValue.b;
            colorValues[3] = newValue.a;

            InvokeEvents(invoke);
        }

        public void SetValue(ColorValue newValue, bool invoke = true)
        {
            colorValues[0] = newValue.r;
            colorValues[1] = newValue.g;
            colorValues[2] = newValue.b;
            colorValues[3] = newValue.a;

            InvokeEvents(invoke);
        }

        public void SetValue(float r, float g, float b, float a, bool invoke = true)
        {
            colorValues[0] = r;
            colorValues[1] = g;
            colorValues[2] = b;
            colorValues[3] = a;

            InvokeEvents(invoke);
        }

        public float GetR()
        {
            return r;
        }

        public float GetG()
        {
            return g;
        }

        public float GetB()
        {
            return b;
        }

        public float GetA()
        {
            return a;
        }

        public void SetR(float r, bool invokeEvents = true)
        {
            colorValues[0] = r;
            InvokeEvents(invokeEvents);
        }

        public void SetG(float g, bool invokeEvents = true)
        {
            colorValues[1] = g;
            InvokeEvents(invokeEvents);
        }

        public void SetB(float b, bool invokeEvents = true)
        {
            colorValues[2] = b;
            InvokeEvents(invokeEvents);
        }

        public void SetA(float a, bool invokeEvents = true)
        {
            colorValues[3] = a;
            InvokeEvents(invokeEvents);
        }

        // Comparison Methods
        public bool IsEqual(ColorValue other)
        {
            return Value == other.Value;
        }

        public bool IsNotEqual(ColorValue other)
        {
            return Value != other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is ColorValue colorValue &&
                   Value == colorValue.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
            //return $"{{\"r\":{Value.r},\"g\":{Value.g},\"b\":{Value.b},\"a\":{Value.a}}}";
        }

        public static ColorValue FromJson(string json)
        {
            ColorValue result = new ColorValue();
            try
            {
                var data = JsonUtility.FromJson<ColorValue>(json);
                result.SetValue(data);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }


        // Operator Overloads
        public static ColorValue operator +(ColorValue a, ColorValue b)
        {
            return new ColorValue(a.Value + b.Value);
        }

        public static ColorValue operator -(ColorValue a, ColorValue b)
        {
            return new ColorValue(a.Value - b.Value);
        }

        public static ColorValue operator *(ColorValue a, ColorValue b)
        {
            return new ColorValue(new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a));
        }

        public static ColorValue operator /(ColorValue a, ColorValue b)
        {
            if (b.r == 0 || b.g == 0 || b.b == 0 || b.a == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            return new ColorValue(new Color(a.r / b.r, a.g / b.g, a.b / b.b, a.a / b.a));
        }

        public static bool operator ==(ColorValue a, ColorValue b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(ColorValue a, ColorValue b)
        {
            return a.Value != b.Value;
        }
    }
}
