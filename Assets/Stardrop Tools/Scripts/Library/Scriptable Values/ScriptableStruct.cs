
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Struct")]
    public class ScriptableStruct<T> : ScriptableValue where T : struct
    {
        [SerializeField] T value;
        [SerializeField] T startValue;

        public override void Default()
        {
            value = startValue;
        }

        public T Value => value;

        public T SetValue(T value) => this.value = value;
    }
}