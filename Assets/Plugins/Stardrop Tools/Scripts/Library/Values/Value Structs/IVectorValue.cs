

using UnityEngine;

namespace StardropTools.Values
{
    public interface IVectorValue<T> : IValue<T>
    {
        public Vector2 ToVector2();
        public Vector3 ToVector3();
        public Vector4 ToVector4();
    }
}
