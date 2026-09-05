using UnityEngine;

public static class CounterTransfer
{
    public static bool TryTransfer(IContainer source, IContainer destination)
    {
        //  Guard against nulls and identical instances
        if (source is null || destination is null || ReferenceEquals(source, destination))
        {
            return false;
        }

        //  Guard against pre-occupied destination (avoids unnecessary removal attempt)
        if (!destination.IsEmpty)
        {
            return false;
        }

        //  Attempt removal directly (atomic operation handling empty state)
        if (!source.TryRemove(out GameObject item))
        {
            return false;
        }

        //  Attempt addition to destination
        if (destination.TryAdd(item))
        {
            return true;
        }

        //  Rollback attempt with fallback logging
        if (!source.TryAdd(item))
        {
            // Critical warning: Item was removed from source but couldn't be added to destination OR returned to source.
            Debug.LogError($"[StationTransferer] Transfer failed and rollback lost item '{item.name}'.");
        }

        return false;
    }
}