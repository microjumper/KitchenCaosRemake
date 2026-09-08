using UnityEngine;

public static class ItemTransfer
{
    public static bool TryTransfer(IContainer source, IContainer destination)
    {
        //  Guard against nulls and identical instances
        if (source is null || destination is null || ReferenceEquals(source, destination))
        {
            Debug.LogWarning($"[{nameof(ItemTransfer)}] Invalid transfer attempt: source or destination is null, or both are the same instance.");
            
            return false;
        }

        //  Guard against pre-occupied destination (avoids unnecessary removal attempt)
        if (destination.HasItem)
        {
            Debug.LogWarning($"[{nameof(ItemTransfer)}] Transfer failed: destination already has an item.");
            
            return false;
        }

        //  Attempt removal directly (atomic operation handling empty state)
        if (!source.TryRetrieve(out KitchenItem item))
        {
            Debug.LogWarning($"[{nameof(ItemTransfer)}] Transfer failed: source has no item to transfer.");

            return false;
        }

        //  Attempt addition to destination
        if (destination.TryStore(item))
        {
            return true;
        }

        //  Rollback attempt with fallback logging
        if (!source.TryStore(item))
        {
            // Critical warning: Item was removed from source but couldn't be added to destination OR returned to source.
            Debug.LogError($"[{nameof(ItemTransfer)}] Transfer failed and rollback lost item '{item.name}'.");
        }

        Debug.LogWarning($"[{nameof(ItemTransfer)}] Transfer failed: destination could not store the item.");
        
        return false;
    }
}