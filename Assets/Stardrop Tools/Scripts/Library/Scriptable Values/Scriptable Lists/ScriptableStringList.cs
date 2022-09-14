
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List String")]
    public class ScriptableStringList : ScriptableValue
    {
        [SerializeField] List<string> defaultList;
        [SerializeField] List<string> list;

        public override void Default()
        {
            list.Clear();

            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);
        }

        public string GetString(int index) => list[index];

        public string GetRandom() => list.GetRandom();

        public List<string> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(string value) => list.Add(value);

        public void AddSafe(string value)
        {
            if (list.Contains(value) == false)
                list.Add(value);
        }

        public void Remove(string value) => list.Remove(value);

        public void RemoveSafe(string value)
        {
            if (list.Contains(value))
                list.Remove(value);
        }

        public string[] ToArray() => list.ToArray();

        public void Clear() => list.Clear();
    }
}