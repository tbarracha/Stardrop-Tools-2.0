
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable String")]
    public class ScriptableString : ScriptableValue
    {
        [SerializeField] string value;
        [SerializeField] string startValue;

        public string Value => value;

        public override void Default()
        {
            value = startValue;
        }

        public void SetValue(string value) => this.value = value;
    }
}