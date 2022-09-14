
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Float")]
    public class ScriptableFloat : ScriptableValue
    {
        [SerializeField] float value;
        [SerializeField] float startValue;

        public float Float => value;

        public override void Default()
        {
            value = startValue;
        }

        public void SetFloat(float value) => this.value = value;
    }
}