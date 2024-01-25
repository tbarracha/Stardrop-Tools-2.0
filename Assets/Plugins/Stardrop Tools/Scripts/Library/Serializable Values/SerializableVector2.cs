
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public struct SerializableVector2
    {
        public float x, y;

        public Vector2 Vector2 => this.ToVector2();
        public Vector3 Vector3 => this.ToVector3();

        public static SerializableVector2 Zero => new SerializableVector2(0, 0);
        public static SerializableVector2 One => new SerializableVector2(1, 1);


        public SerializableVector2(Transform transform)
        {
            this.x = transform.position.x;
            this.y = transform.position.y;
        }
        public SerializableVector2(Transform transform, bool localPosition)
        {
            if (localPosition == false)
            {
                this.x = transform.position.x;
                this.y = transform.position.y;
            }
            else
            {
                this.x = transform.localPosition.x;
                this.y = transform.localPosition.y;
            }
        }

        public SerializableVector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public SerializableVector2(Vector2 vector2)
        {
            x = vector2.x;
            y = vector2.y;
        }

        public SerializableVector2(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
        }

        public override string ToString()
        {
            return $"{x}{Serializables.SEPERATOR}{y}";
        }

        public static SerializableVector2 TryParse(string str)
        {
            string[] parts = str.Split(Serializables.SEPERATOR);
            if (parts.Length == 2 &&
                float.TryParse(parts[0], out float parsedX) &&
                float.TryParse(parts[1], out float parsedY))
            {
                return new SerializableVector2(parsedX, parsedY);
            }

            // Handle parsing error, return a default value, or throw an exception
            Debug.LogError($"Failed to parse SerializableVector3 from string: {str}");
            return Zero;
        }
    }
}