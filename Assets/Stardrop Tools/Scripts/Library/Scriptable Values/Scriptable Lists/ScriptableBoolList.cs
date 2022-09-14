
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List Bool")]
    public class ScriptableBoolList : ScriptableValue
    {
        [SerializeField] List<bool> defaultList;
        [SerializeField] List<bool> list;

        public override void Default()
        {
            list.Clear();

            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);
        }

        public bool GetBool(int index) => list[index];

        public bool GetRandom() => list.GetRandom();

        public List<bool> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(bool value) => list.Add(value);

        public void AddSafe(bool value)
        {
            if (list.Contains(value) == false)
                list.Add(value);
        }

        public void Remove(bool value) => list.Remove(value);

        public void RemoveSafe(bool value)
        {
            if (list.Contains(value))
                list.Remove(value);
        }

        public bool[] ToArray() => list.ToArray();

        public void Clear() => list.Clear();
    }
}