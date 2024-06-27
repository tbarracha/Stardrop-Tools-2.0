using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct Vector2Value : IVectorValue<Vector2>
    {
        private readonly float[] values;

        private readonly EventCallback<Vector2> onValueChanged;
        public EventCallback<Vector2> OnValueChanged => onValueChanged;



        // Properties
        // --------------------------------------------------------------------------

        public Vector2 Value
        {
            get => new Vector2(values[0], values[1]);
            set => SetValue(value);
        }

        public float x
        {
            get => values[0];
            set => SetX(value);
        }

        public float y
        {
            get => values[1];
            set => SetY(value);
        }

        public Vector2 ToVector2()
        {
            return Value;
        }

        public Vector3 ToVector3()
        {
            return Value;
        }

        public Vector4 ToVector4()
        {
            return Value;
        }



        // Constructors
        // --------------------------------------------------------------------------

        public Vector2Value(Vector2 value)
        {
            this.values = new float[2] { value.x, value.y };
            this.onValueChanged = new EventCallback<Vector2>();
        }

        public Vector2Value(Vector2Value value)
        {
            this.values = new float[2] { value.x, value.y };
            this.onValueChanged = new EventCallback<Vector2>();
        }

        public Vector2Value(float x, float y)
        {
            this.values = new float[2] { x, y };
            this.onValueChanged = new EventCallback<Vector2>();
        }

        public Vector2Value(Transform transform, bool isLocalPosition)
        {
            Vector3 value = isLocalPosition ? transform.position : transform.localPosition;

            this.values = new float[2] { value.x, value.y };
            this.onValueChanged = new EventCallback<Vector2>();
        }



        // Methods
        // --------------------------------------------------------------------------

        // Event invocation
        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(Value);
        }

        public Vector2 GetValue()
        {
            return Value;
        }

        public void SetValue(Vector2 newValue, bool invoke = true)
        {
            values[0] = newValue.x;
            values[1] = newValue.y;

            InvokeEvents(invoke);
        }

        public void SetValue(Vector2Value newValue, bool invoke = true)
        {
            values[0] = newValue.x;
            values[1] = newValue.y;

            InvokeEvents(invoke);
        }

        public void SetValue(float x, float y, bool invoke = true)
        {
            values[0] = x;
            values[1] = y;

            InvokeEvents(invoke);
        }

        public float GetX()
        {
            return values[0];
        }

        public float GetY()
        {
            return values[1];
        }

        public void SetX(float x, bool invokeEvents = true)
        {
            values[0] = x;
            InvokeEvents(invokeEvents);
        }

        public void SetY(float y, bool invokeEvents = true)
        {
            values[1] = y;
            InvokeEvents(invokeEvents);
        }



        // Mathematical Operations
        // --------------------------------------------------------------------------

        public Vector2Value Add(Vector2Value other, bool invokeEvents = true)
        {
            values[0] += other.x;
            values[1] += other.y;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector2Value Subtract(Vector2Value other, bool invokeEvents = true)
        {
            values[0] -= other.x;
            values[1] -= other.y;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector2Value Multiply(Vector2Value other, bool invokeEvents = true)
        {
            values[0] *= other.x;
            values[1] *= other.y;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector2Value Divide(Vector2Value other, bool invokeEvents = true)
        {
            if (other.x == 0 || other.y == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return this;
            }

            values[0] /= other.x;
            values[1] /= other.y;
            InvokeEvents(invokeEvents);
            return this;
        }



        // Comparison Methods
        // --------------------------------------------------------------------------

        public bool IsEqual(Vector2Value other)
        {
            return Value == other.Value;
        }

        public bool IsNotEqual(Vector2Value other)
        {
            return Value != other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2Value vector2Value &&
                   Value == vector2Value.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
            //return $"{{\"x\":{Value.x},\"y\":{Value.y}}}";
        }

        public static Vector2Value FromJson(string json)
        {
            Vector2Value result = new Vector2Value();
            try
            {
                var data = JsonUtility.FromJson<Vector2Value>(json);
                result.SetValue(data);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }



        // Operator Overloads
        // --------------------------------------------------------------------------

        public static Vector2Value operator +(Vector2Value a, Vector2Value b)
        {
            return new Vector2Value(a.Value + b.Value);
        }

        public static Vector2Value operator -(Vector2Value a, Vector2Value b)
        {
            return new Vector2Value(a.Value - b.Value);
        }

        public static Vector2Value operator *(Vector2Value a, Vector2Value b)
        {
            return new Vector2Value(new Vector2(a.Value.x * b.Value.x, a.Value.y * b.Value.y));
        }

        public static Vector2Value operator /(Vector2Value a, Vector2Value b)
        {
            if (b.x == 0 || b.y == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            return new Vector2Value(new Vector2(a.Value.x / b.Value.x, a.Value.y / b.Value.y));
        }

        public static bool operator ==(Vector2Value a, Vector2Value b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(Vector2Value a, Vector2Value b)
        {
            return a.Value != b.Value;
        }

        public static bool operator >(Vector2Value a, Vector2Value b)
        {
            return a.Value.magnitude > b.Value.magnitude;
        }

        public static bool operator <(Vector2Value a, Vector2Value b)
        {
            return a.Value.magnitude < b.Value.magnitude;
        }

        public static bool operator >=(Vector2Value a, Vector2Value b)
        {
            return a.Value.magnitude >= b.Value.magnitude;
        }

        public static bool operator <=(Vector2Value a, Vector2Value b)
        {
            return a.Value.magnitude <= b.Value.magnitude;
        }
    }
}
