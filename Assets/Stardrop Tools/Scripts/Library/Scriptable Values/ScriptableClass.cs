
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Class")]
    public class ScriptableClass<T> : ScriptableValue where T : class
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