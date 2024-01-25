
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public struct SerializableVector3
    {
        public float x, y, z;

        public Vector2 Vector2 => this.ToVector2();
        public Vector3 Vector3 => this.ToVector3();

        public static SerializableVector3 Zero => new SerializableVector3(0, 0, 0);
        public static SerializableVector3 One => new SerializableVector3(1, 1, 1);


        public SerializableVector3(Transform transform)
        {
            this.x = transform.position.x;
            this.y = transform.position.y;
            this.z = transform.position.z;
        }
        public SerializableVector3(Transform transform, bool localPosition)
        {
            if (localPosition == false)
            {
                this.x = transform.position.x;
                this.y = transform.position.y;
                this.z = transform.position.z;
            }
            else
            {
                this.x = transform.localPosition.x;
                this.y = transform.localPosition.y;
                this.z = transform.localPosition.z;
            }
        }

        public SerializableVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public SerializableVector3(Vector3 vector3)
        {
            x = vector3.x;
            y = vector3.y;
            z = vector3.z;
        }

        public SerializableVector3(Vector2 vector2)
        {
            x = vector2.x;
            y = vector2.y;
            z = 0;
        }

        public override string ToString()
        {
            return $"{x}{Serializables.SEPERATOR}{y}{Serializables.SEPERATOR}{z}";
        }

        public static SerializableVector3 TryParse(string str)
        {
            string[] parts = str.Split(Serializables.SEPERATOR);
            if (parts.Length == 3 &&
                float.TryParse(parts[0], out float parsedX) &&
                float.TryParse(parts[1], out float parsedY) &&
                float.TryParse(parts[2], out float parsedZ))
            {
                return new SerializableVector3(parsedX, parsedY, parsedZ);
            }

            // Handle parsing error, return a default value, or throw an exception
            Debug.LogError($"Failed to parse SerializableVector3 from string: {str}");
            return Zero;
        }
    }
}