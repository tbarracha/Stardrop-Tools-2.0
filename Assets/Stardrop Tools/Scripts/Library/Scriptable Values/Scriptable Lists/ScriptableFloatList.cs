
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List Float")]
    public class ScriptableFloatList : ScriptableValue
    {
        [SerializeField] List<float> defaultList;
        [SerializeField] List<float> list;

        public override void Default()
        {
            list.Clear();

            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);
        }

        public float GetFloat(int index) => list[index];

        public float GetRandom() => list.GetRandom();

        public List<float> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(float value) => list.Add(value);

        public void AddSafe(float value)
        {
            if (list.Contains(value) == false)
                list.Add(value);
        }

        public void Remove(float value) => list.Remove(value);

        public void RemoveSafe(float value)
        {
            if (list.Contains(value))
                list.Remove(value);
        }

        public float[] ToArray() => list.ToArray();

        public void Clear() => list.Clear();
    }
}