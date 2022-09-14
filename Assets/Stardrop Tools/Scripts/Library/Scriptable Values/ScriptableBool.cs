
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Bool")]
    public class ScriptableBool : ScriptableValue
    {
        [SerializeField] bool value;
        [SerializeField] bool startValue;

        public override void Default()
        {
            value = startValue;
        }

        public bool Bool => value;

        public bool SetBool(bool value) => this.value = value;
    }
}