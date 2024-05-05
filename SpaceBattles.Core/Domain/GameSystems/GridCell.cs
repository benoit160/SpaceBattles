namespace SpaceBattles.Core.Domain.GameSystems;

using System.Diagnostics.CodeAnalysis;

public class GridCell<T>
    where T : class, IGridItem
{
    public T? Item { get; private set; }

    public bool IsEmpty => Item is null;

    /// <summary>
    /// Inserts a  new item in the slot.
    /// </summary>
    public bool Insert(T item)
    {
        if (Item is null)
        {
            Item = item;
            return true;
        }

        return false;
    }

    /// <summary>
    /// Delete the item from the slot.
    /// </summary>
    public void Clear()
    {
        Item = null;
    }

    /// <summary>
    /// Tries to remove the item from the slot, and gives it back.
    /// </summary>
    public bool TryRemove([NotNullWhen(true)]out T? item)
    {
        item = default;
        if (Item is null || !Item.CanBeRemoved) return false;

        item = Item;
        Item = default;
        return true;
    }
}