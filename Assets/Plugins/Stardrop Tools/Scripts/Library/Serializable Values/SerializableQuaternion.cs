
using UnityEngine;

namespace StardropTools
{
    [System.Serializable]
    public struct SerializableQuaternion
    {
        public float x, y, z, w;

        public Quaternion Quaternion => this.ToQuaternion();

        public static SerializableQuaternion Identity => new SerializableQuaternion(0, 0, 0, 0);

        public SerializableQuaternion(Transform transform)
        {
            this.x = transform.rotation.x;
            this.y = transform.rotation.y;
            this.z = transform.rotation.z;
            this.w = transform.rotation.w;
        }

        public SerializableQuaternion(Transform transform, bool localRotation)
        {
            if (localRotation == false)
            {
                this.x = transform.rotation.x;
                this.y = transform.rotation.y;
                this.z = transform.rotation.z;
                this.w = transform.rotation.w;
            }
            else
            {
                this.x = transform.localRotation.x;
                this.y = transform.localRotation.y;
                this.z = transform.localRotation.z;
                this.w = transform.localRotation.w;
            }
        }

        public SerializableQuaternion(Quaternion quaternion)
        {
            this.x = quaternion.x;
            this.y = quaternion.y;
            this.z = quaternion.z;
            this.w = quaternion.w;
        }

        public SerializableQuaternion(Vector3 euler)
        {
            Quaternion quaternion = Quaternion.Euler(euler);

            this.x = quaternion.x;
            this.y = quaternion.y;
            this.z = quaternion.z;
            this.w = quaternion.w;
        }

        public SerializableQuaternion(float x, float y, float z, float w)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public override string ToString()
        {
            return $"{x}{Serializables.SEPERATOR}{y}{Serializables.SEPERATOR}{z}{Serializables.SEPERATOR}{w}";
        }

        public static SerializableQuaternion TryParse(string str)
        {
            string[] parts = str.Split(Serializables.SEPERATOR);
            if (parts.Length == 4 &&
                float.TryParse(parts[0], out float parsedX) &&
                float.TryParse(parts[1], out float parsedY) &&
                float.TryParse(parts[2], out float parsedZ) &&
                float.TryParse(parts[3], out float parsedW))
            {
                return new SerializableQuaternion(parsedX, parsedY, parsedZ, parsedW);
            }

            // Handle parsing error, return a default value, or throw an exception
            Debug.LogError($"Failed to parse SerializableQuaternion from string: {str}");
            return Identity;
        }
    }
}