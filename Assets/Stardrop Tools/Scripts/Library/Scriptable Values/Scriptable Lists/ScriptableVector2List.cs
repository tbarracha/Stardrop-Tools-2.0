
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List Vector2")]
    public class ScriptableVector2List : ScriptableValue
    {
        [SerializeField] List<Vector2> defaultList;
        [SerializeField] List<Vector2> list;

        public override void Default()
        {
            list.Clear();

            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);
        }

        public Vector2 GetVector2(int index) => list[index];

        public Vector2 GetRandom() => list.GetRandom();

        public List<Vector2> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(Vector2 value) => list.Add(value);

        public void AddSafe(Vector2 value)
        {
            if (list.Contains(value) == false)
                list.Add(value);
        }

        public void Remove(Vector2 value) => list.Remove(value);

        public void RemoveSafe(Vector2 value)
        {
            if (list.Contains(value))
                list.Remove(value);
        }

        public Vector2[] ToArray() => list.ToArray();

        public void Clear() => list.Clear();
    }
}