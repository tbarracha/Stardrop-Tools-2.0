using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct Vector4Value : IVectorValue<Vector4>
    {
        private readonly float[] values;

        private readonly EventCallback<Vector4> onValueChanged;
        public EventCallback<Vector4> OnValueChanged => onValueChanged;

        public Vector4 Value
        {
            get => new Vector4(values[0], values[1], values[2], values[3]);
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

        public float z
        {
            get => values[2];
            set => SetZ(value);
        }

        public float w
        {
            get => values[3];
            set => SetW(value);
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
        // ---------------------------------------------------------------------------------------
        public Vector4Value(Vector4 value)
        {
            this.values = new float[4] { value.x, value.y, value.z, value.w };
            this.onValueChanged = new EventCallback<Vector4>();
        }

        public Vector4Value(Vector4Value value)
        {
            this.values = new float[4] { value.x, value.y, value.z, value.w };
            this.onValueChanged = new EventCallback<Vector4>();
        }

        public Vector4Value(float x, float y, float z, float w)
        {
            this.values = new float[4] { x, y, z, w };
            this.onValueChanged = new EventCallback<Vector4>();
        }

        public Vector4Value(Transform transform, bool isLocalPosition)
        {
            Vector4 value = isLocalPosition ? transform.position : transform.localPosition;

            this.values = new float[2] { value.x, value.y };
            this.onValueChanged = new EventCallback<Vector4>();
        }



        // Methods
        // ---------------------------------------------------------------------------------------
        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(Value);
        }

        public Vector4 GetValue()
        {
            return Value;
        }

        public void SetValue(Vector4 newValue, bool invoke = true)
        {
            values[0] = newValue.x;
            values[1] = newValue.y;
            values[2] = newValue.z;
            values[3] = newValue.w;

            InvokeEvents(invoke);
        }

        public void SetValue(Vector4Value newValue, bool invoke = true)
        {
            values[0] = newValue.x;
            values[1] = newValue.y;
            values[2] = newValue.z;
            values[3] = newValue.w;

            InvokeEvents(invoke);
        }

        public void SetValue(float x, float y, float z, float w, bool invoke = true)
        {
            values[0] = x;
            values[1] = y;
            values[2] = z;
            values[3] = w;

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

        public float GetZ()
        {
            return values[2];
        }

        public float GetW()
        {
            return values[3];
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

        public void SetZ(float z, bool invokeEvents = true)
        {
            values[2] = z;
            InvokeEvents(invokeEvents);
        }

        public void SetW(float w, bool invokeEvents = true)
        {
            values[3] = w;
            InvokeEvents(invokeEvents);
        }



        // Mathematical Operations
        // ---------------------------------------------------------------------------------------
        public Vector4Value Add(Vector4Value other, bool invokeEvents = true)
        {
            values[0] += other.x;
            values[1] += other.y;
            values[2] += other.z;
            values[3] += other.w;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector4Value Subtract(Vector4Value other, bool invokeEvents = true)
        {
            values[0] -= other.x;
            values[1] -= other.y;
            values[2] -= other.z;
            values[3] -= other.w;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector4Value Multiply(Vector4Value other, bool invokeEvents = true)
        {
            values[0] *= other.x;
            values[1] *= other.y;
            values[2] *= other.z;
            values[3] *= other.w;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector4Value Divide(Vector4Value other, bool invokeEvents = true)
        {
            if (other.x == 0 || other.y == 0 || other.z == 0 || other.w == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return this;
            }

            values[0] /= other.x;
            values[1] /= other.y;
            values[2] /= other.z;
            values[3] /= other.w;
            InvokeEvents(invokeEvents);
            return this;
        }



        // Comparison Methods
        // ---------------------------------------------------------------------------------------
        public bool IsEqual(Vector4Value other)
        {
            return Value == other.Value;
        }

        public bool IsNotEqual(Vector4Value other)
        {
            return Value != other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4Value vector4Value &&
                   Value == vector4Value.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
            //return $"{{\"x\":{Value.x},\"y\":{Value.y},\"z\":{Value.z},\"w\":{Value.w}}}";
        }

        public static Vector4Value FromJson(string json)
        {
            Vector4Value result = new Vector4Value();
            try
            {
                var data = JsonUtility.FromJson<Vector4Value>(json);
                result.SetValue(data);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON: {ex.Message}");
            }
            return result;
        }



        // Operator Overloads
        // ---------------------------------------------------------------------------------------
        public static Vector4Value operator +(Vector4Value a, Vector4Value b)
        {
            return new Vector4Value(a.Value + b.Value);
        }

        public static Vector4Value operator -(Vector4Value a, Vector4Value b)
        {
            return new Vector4Value(a.Value - b.Value);
        }

        public static Vector4Value operator *(Vector4Value a, Vector4Value b)
        {
            return new Vector4Value(new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w));
        }

        public static Vector4Value operator /(Vector4Value a, Vector4Value b)
        {
            if (b.x == 0 || b.y == 0 || b.z == 0 || b.w == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            return new Vector4Value(new Vector4(a.x / b.x, a.y / b.y, a.z / b.z, a.w / b.w));
        }

        public static bool operator ==(Vector4Value a, Vector4Value b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(Vector4Value a, Vector4Value b)
        {
            return a.Value != b.Value;
        }

        public static bool operator >(Vector4Value a, Vector4Value b)
        {
            return a.Value.magnitude > b.Value.magnitude;
        }

        public static bool operator <(Vector4Value a, Vector4Value b)
        {
            return a.Value.magnitude < b.Value.magnitude;
        }

        public static bool operator >=(Vector4Value a, Vector4Value b)
        {
            return a.Value.magnitude >= b.Value.magnitude;
        }

        public static bool operator <=(Vector4Value a, Vector4Value b)
        {
            return a.Value.magnitude <= b.Value.magnitude;
        }
    }
}
