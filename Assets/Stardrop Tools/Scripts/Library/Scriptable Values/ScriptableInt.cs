
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable Int")]
    public class ScriptableInt : ScriptableValue
    {
        [SerializeField] int value;
        [SerializeField] int startValue;

        public int Int => value;

        public override void Default()
        {
            value = startValue;
        }

        public void SetInt(int value) => this.value = value;
    }
}