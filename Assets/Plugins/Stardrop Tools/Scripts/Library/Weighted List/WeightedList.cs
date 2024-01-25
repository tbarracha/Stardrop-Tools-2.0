
using System.Collections.Generic;

namespace StardropTools
{
    /// <summary>
    /// The WeightedList class, provides a versatile solution for creating and managing lists of elements with associated weights, allowing for weighted randomization.
    /// <para>Example (pseudo code synthax):</para> 
    /// <para>WeightedList(EnemyType): {Type=basic, weight=5; Type=rare, weight=2 }</para>
    /// </summary>
    [System.Serializable]
    public class WeightedList<T>
    {
#if UNITY_ENGINE
        [ResizableTextArea][UnityEngine.SerializeField] string description = "Insert Weight Desciption...";
#endif
        public List<WeightedItem<T>> list = new List<WeightedItem<T>>();

        public int Count { get => list.Count; }
        public T RandomValue { get => GetRandom(); }



        #region Constructors
        
        public WeightedList() { }

        public WeightedList(List<WeightedItem<T>> itemList)
            => list = itemList;

        public WeightedList(WeightedItem<T>[] itemArray)
            => list = itemArray.ToList();

        public WeightedList(T[] items)
        {
            list = new List<WeightedItem<T>>();

            int weight = items.Length;
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null)
                {
                    list.Add(new WeightedItem<T>(items[i], weight));
                    weight--;
                }
            }
        }

        public WeightedList(T[] items, int[] weights)
        {
            list = new List<WeightedItem<T>>();

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] != null && weights[i] != null)
                {
                    list.Add(new WeightedItem<T>(items[i], weights[i]));
                }

                else
                {
                    UnityEngine.Debug.Log($"WeightedList of type: {typeof(T)} has Item or Weight is null at index: {i}");
                    break;
                }
            }
        }

        #endregion // Constructors


        public WeightedItem<T> GetAtIndex(int index) => list[index];

        public void Add(T item, int weight)
            => list.Add(new WeightedItem<T>(item, weight));

        public void Add(WeightedItem<T> item)
        {
            if (list.Contains(item) == false)
                list.Add(item);
        }

        public void Remove(WeightedItem<T> item)
        {
            if (list.Contains(item) == false)
                list.Remove(item);
        }

        public T GetRandom()
        {
            if (list.Count == 0)
            {
                UnityEngine.Debug.Log("Weight List is empty!");
                return default(T);
            }

            float totalWeight = 0;

            foreach (WeightedItem<T> item in list)
                totalWeight += item.Weight;            

            float value = UnityEngine.Random.value * totalWeight;

            float sumWeight = 0;

            foreach (WeightedItem<T> item in list)
            {
                sumWeight += item.Weight;

                if (sumWeight >= value)
                    return item.Item;
            }

            return default(T);
        }

        public List<T> GetRandomAmount(int amount)
        {
            var list = new List<T>();

            for (int i = 0; i < amount; i++)
                list.Add(GetRandom());

            return list;
        }
    }
}