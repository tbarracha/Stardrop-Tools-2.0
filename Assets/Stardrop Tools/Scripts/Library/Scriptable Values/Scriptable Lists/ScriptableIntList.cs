
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List Int")]
    public class ScriptableIntList : ScriptableValue
    {
        [SerializeField] List<int> defaultList;
        [SerializeField] List<int> list;

        public override void Default()
        {
            list.Clear();

            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);
        }

        public int GetInt(int index) => list[index];

        public int GetRandom() => list.GetRandom();

        public List<int> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(int value) => list.Add(value);

        public void AddSafe(int value)
        {
            if (list.Contains(value) == false)
                list.Add(value);
        }

        public void Remove(int value) => list.Remove(value);

        public void RemoveSafe(int value)
        {
            if (list.Contains(value))
                list.Remove(value);
        }

        public int[] ToArray() => list.ToArray();

        public void Clear() => list.Clear();
    }
}