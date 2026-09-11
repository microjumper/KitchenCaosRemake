using System;
using System.Collections.Immutable;
using UnityEngine;

public class DeliveryCounter : MonoBehaviour, IInteractable
{
    public event Action<ImmutableHashSet<KitchenItemDefinition>> Delivered;

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.Item != null && otherContainer.Item is Plate plate)
        {
            Delivered?.Invoke(plate.PlatedItems.ToImmutableHashSet());

            otherContainer.TryRetrieve(out KitchenItem item);

            Destroy(item.gameObject);

            return true;
        }

        return false;
    }
}
