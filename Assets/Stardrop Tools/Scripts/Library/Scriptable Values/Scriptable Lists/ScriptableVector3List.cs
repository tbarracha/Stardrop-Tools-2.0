
using System.Collections.Generic;
using UnityEngine;

namespace StardropTools
{
    [CreateAssetMenu(menuName = "Stardrop / Scriptable Values / Scriptable List / Scriptable List Vector3")]
    public class ScriptableVector3List : ScriptableValue
    {
        [SerializeField] List<Vector3> defaultList;
        [SerializeField] List<Vector3> list;

        public override void Default()
        {
            list.Clear();

            for (int i = 0; i < defaultList.Count; i++)
                list.Add(defaultList[i]);
        }

        public Vector3 GetVector3(int index) => list[index];

        public Vector3 GetRandom() => list.GetRandom();

        public List<Vector3> GetRandomNonRepeat(int amount) => list.GetRandomNonRepeat(amount);

        public void Add(Vector3 value) => list.Add(value);

        public void AddSafe(Vector3 value)
        {
            if (list.Contains(value) == false)
                list.Add(value);
        }

        public void Remove(Vector3 value) => list.Remove(value);

        public void RemoveSafe(Vector3 value)
        {
            if (list.Contains(value))
                list.Remove(value);
        }

        public Vector3[] ToArray() => list.ToArray();

        public void Clear() => list.Clear();
    }
}