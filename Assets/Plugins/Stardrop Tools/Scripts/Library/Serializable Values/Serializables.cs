using UnityEngine;

namespace StardropTools
{
    public static class Serializables
    {
        public const char SEPERATOR = ';';

        // Vector2
        public static Vector2 ToVector2(this SerializableVector2 vector)
            => new Vector2(vector.x, vector.y);

        public static Vector2 ToVector2(this SerializableVector3 vector)
            => new Vector2(vector.x, vector.y);

        public static SerializableVector2 ToSerializableVector2(this Vector2 vector)
            => new SerializableVector2(vector.x, vector.y);

        public static SerializableVector2[] ToSerializableVector2Array(this Vector2[] vectorArray)
        {
            SerializableVector2[] array = new SerializableVector2[vectorArray.Length];

            for (int i = 0; i < vectorArray.Length; i++)
                array[i] = vectorArray[i].ToSerializableVector2();

            return array;
        }



        // Vector3
        public static Vector3 ToVector3(this SerializableVector2 vector)
            => new Vector3(vector.x, vector.y, 0);

        public static Vector3 ToVector3(this SerializableVector3 vector)
            => new Vector3(vector.x, vector.y, vector.z);

        public static SerializableVector3 ToSerializableVector3(this Vector3 vector)
            => new SerializableVector3(vector.x, vector.y, vector.z);

        public static SerializableVector3[] ToSerializableVector3Array(this Vector3[] vectorArray)
        {
            SerializableVector3[] array = new SerializableVector3[vectorArray.Length];

            for (int i = 0; i < vectorArray.Length; i++)
                array[i] = vectorArray[i].ToSerializableVector3();

            return array;
        }



        // Quaternion
        public static Quaternion ToQuaternion(this SerializableQuaternion quaternion)
            => new Quaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);

        public static SerializableQuaternion ToSerializableQuaternion(this Quaternion quaternion)
            => new SerializableQuaternion(quaternion.x, quaternion.y, quaternion.z, quaternion.w);

        public static SerializableQuaternion[] ToSerializableQuaternionArray(this Quaternion[] quaternionArray)
        {
            SerializableQuaternion[] array = new SerializableQuaternion[quaternionArray.Length];

            for (int i = 0; i < quaternionArray.Length; i++)
                array[i] = quaternionArray[i].ToSerializableQuaternion();

            return array;
        }
    }
}
