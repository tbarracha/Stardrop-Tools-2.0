
namespace StardropTools
{
    [System.Serializable]
    public struct WeightedItem<T>
    {
        [UnityEngine.SerializeField] private T item;
        [UnityEngine.SerializeField] [UnityEngine.Min(0)] private int weight;

        public T Item => item;
        public int Weight => weight;

        public WeightedItem(T item, int weight)
        {
            this.item = item;
            this.weight = weight;
        }

        internal void SetItem(T item) => this.item = item;
        internal void SetWeight(int weight) => this.weight = weight;
    }
}