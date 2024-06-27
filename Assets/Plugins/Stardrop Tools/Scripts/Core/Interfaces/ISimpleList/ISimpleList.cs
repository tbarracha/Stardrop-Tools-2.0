
namespace StardropTools
{
    public interface ISimpleList<T>
    {
        int Count { get; }

        // Add an item to the list
        void Add(T item);

        // Remove the first occurrence of an item from the list
        bool Remove(T item);

        // Clear all items from the list
        void Clear();

        // Check if the list contains a specific item
        bool Contains(T item);

        // Get the index of the first occurrence of an item in the list
        int IndexOf(T item);

        // Remove an item at a specific index from the list
        void RemoveAt(int index);
    }
}
