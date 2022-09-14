
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Vector 2")]
    public class ScriptableVector2 : ScriptableValue
    {
        [SerializeField] Vector2 value;
        [SerializeField] Vector2 startValue;

        public Vector2 Value => value;

        public override void Default()
        {
            value = startValue;
        }

        public void SetValue(Vector2 value) => this.value = value;
    }
}