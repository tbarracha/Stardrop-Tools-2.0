
using System;
using UnityEngine;

namespace StardropTools.Values
{
    [System.Serializable]
    public struct Vector3Value : IVectorValue<Vector3>
    {
        private readonly float[] values;

        private readonly EventCallback<Vector3> onValueChanged;
        public EventCallback<Vector3> OnValueChanged => onValueChanged;

        public Vector3 Value
        {
            get => new Vector3(values[0], values[1], values[2]);
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

        public Vector3Value(Vector3 value)
        {
            this.values = new float[3] { value.x, value.y, value.z };
            this.onValueChanged = new EventCallback<Vector3>();
        }

        public Vector3Value(Vector3Value value)
        {
            this.values = new float[3] { value.x, value.y, value.z };
            this.onValueChanged = new EventCallback<Vector3>();
        }

        public Vector3Value(float x, float y, float z)
        {
            this.values = new float[3] { x, y, z };
            this.onValueChanged = new EventCallback<Vector3>();
        }

        public Vector3Value(Transform transform, bool isLocalPosition)
        {
            Vector3 value = isLocalPosition ? transform.position : transform.localPosition;

            this.values = new float[2] { value.x, value.y };
            this.onValueChanged = new EventCallback<Vector3>();
        }



        // Methods
        // --------------------------------------------------------------------------

        // Event invocation
        public void InvokeEvents(bool invoke)
        {
            if (invoke)
                OnValueChanged?.Invoke(Value);
        }

        public Vector3 GetValue()
        {
            return Value;
        }

        public void SetValue(Vector3 newValue, bool invoke = true)
        {
            values[0] = newValue.x;
            values[1] = newValue.y;
            values[2] = newValue.z;

            InvokeEvents(invoke);
        }

        public void SetValue(Vector3Value newValue, bool invoke = true)
        {
            values[0] = newValue.x;
            values[1] = newValue.y;
            values[2] = newValue.z;

            InvokeEvents(invoke);
        }

        public void SetValue(float x, float y, float z, bool invoke = true)
        {
            values[0] = x;
            values[1] = y;
            values[2] = z;

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



        // Mathematical Operations
        // --------------------------------------------------------------------------

        public Vector3Value Add(Vector3Value other, bool invokeEvents = true)
        {
            values[0] += other.x;
            values[1] += other.y;
            values[2] += other.z;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector3Value Subtract(Vector3Value other, bool invokeEvents = true)
        {
            values[0] -= other.x;
            values[1] -= other.y;
            values[2] -= other.z;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector3Value Multiply(Vector3Value other, bool invokeEvents = true)
        {
            values[0] *= other.x;
            values[1] *= other.y;
            values[2] *= other.z;
            InvokeEvents(invokeEvents);
            return this;
        }

        public Vector3Value Divide(Vector3Value other, bool invokeEvents = true)
        {
            if (other.x == 0 || other.y == 0 || other.z == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return this;
            }

            values[0] /= other.x;
            values[1] /= other.y;
            values[2] /= other.z;
            InvokeEvents(invokeEvents);
            return this;
        }



        // Comparisson Methods
        // --------------------------------------------------------------------------

        public bool IsEqual(Vector3Value other)
        {
            return Value == other.Value;
        }

        public bool IsNotEqual(Vector3Value other)
        {
            return Value != other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector3Value vector3Value &&
                   Value == vector3Value.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
            //return $"{{\"x\":{Value.x},\"y\":{Value.y},\"z\":{Value.z}}}";
        }

        public static Vector3Value FromJson(string json)
        {
            Vector3Value result = new Vector3Value();
            try
            {
                var data = JsonUtility.FromJson<Vector3Value>(json);
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

        public static Vector3Value operator +(Vector3Value a, Vector3Value b)
        {
            return new Vector3Value(a.Value + b.Value);
        }

        public static Vector3Value operator -(Vector3Value a, Vector3Value b)
        {
            return new Vector3Value(a.Value - b.Value);
        }

        public static Vector3Value operator *(Vector3Value a, Vector3Value b)
        {
            return new Vector3Value(new Vector3(a.x * b.x, a.y * b.y, a.z * b.z));
        }

        public static Vector3Value operator /(Vector3Value a, Vector3Value b)
        {
            if (b.x == 0 || b.y == 0 || b.z == 0)
            {
                UnityEngine.Debug.LogError("Division by zero!");
                return a;
            }

            return new Vector3Value(new Vector3(a.x / b.x, a.y / b.y, a.z / b.z));
        }

        public static bool operator ==(Vector3Value a, Vector3Value b)
        {
            return a.Value == b.Value;
        }

        public static bool operator !=(Vector3Value a, Vector3Value b)
        {
            return a.Value != b.Value;
        }

        public static bool operator >(Vector3Value a, Vector3Value b)
        {
            return a.Value.magnitude > b.Value.magnitude;
        }

        public static bool operator <(Vector3Value a, Vector3Value b)
        {
            return a.Value.magnitude < b.Value.magnitude;
        }

        public static bool operator >=(Vector3Value a, Vector3Value b)
        {
            return a.Value.magnitude >= b.Value.magnitude;
        }

        public static bool operator <=(Vector3Value a, Vector3Value b)
        {
            return a.Value.magnitude <= b.Value.magnitude;
        }
    }
}
