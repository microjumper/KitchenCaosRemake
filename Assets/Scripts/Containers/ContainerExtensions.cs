using System;
using UnityEngine;

public static class ContainerExtensions
{
    private const string LogHeader = "[ContainerExtensions]";

    public static bool TryTransferTo(this IContainer source, IContainer destination)
    {
        // Guard against nulls and identical instances.
        if (source is null || destination is null || ReferenceEquals(source, destination))
        {
            Debug.LogWarning($"{LogHeader} Invalid transfer attempt: source or destination is null, or both are the same instance.");

            return false;
        }

        // Pre-validation.
        if (source.HeldItem == null)
        {
            Debug.LogWarning($"{LogHeader} Transfer failed: source is empty.");

            return false;
        }

        if (destination.HeldItem != null)
        {
            Debug.LogWarning($"{LogHeader} Transfer failed: destination already has an item.");

            return false;
        }

        // Retrieve the item from the source.
        if (!source.TryRetrieve(out KitchenItem item) || item == null)
        {
            Debug.LogWarning($"{LogHeader} Transfer failed: source could not provide an item.");

            return false;
        }

        // Attempt to store it in the destination.
        try
        {
            if (destination.TryStore(item))
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"{LogHeader} Destination threw an exception during store: {ex}");
        }

        // Destination rejected the item or threw. Attempt to restore the source.
        try
        {
            if (source.TryStore(item))
            {
                Debug.LogWarning($"{LogHeader} Transfer failed: destination rejected item '{item.name}'. Source was successfully restored.");

                return false;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"{LogHeader} CRITICAL: Rollback threw an exception for item '{item.name}': {ex}");
        }

        // Source could not be restored.
        Debug.LogError($"{LogHeader} CRITICAL: Rollback failed. Item '{item.name}' may have been lost.");

        return false;
    }
}