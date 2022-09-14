
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Vector 3")]
    public class ScriptableVector3 : ScriptableValue
    {
        [SerializeField] Vector3 value;
        [SerializeField] Vector3 startValue;

        public Vector3 Value => value;

        public override void Default()
        {
            value = startValue;
        }

        public void SetValue(Vector3 value) => this.value = value;
    }
}