using System.Collections.Generic;
using UnityEngine;

public class Plate : KitchenItem
{
    [SerializeField] private Deliverable platedDeliverable;

    public IReadOnlyCollection<KitchenItemDefinition> PlatedItems => platedDeliverable.ItemSet;

    public bool TryAdd(KitchenItem item)
    {
        var added = platedDeliverable.TryAdd(item.Definition);

        if (added)
        {
            Destroy(item.gameObject);
        }

        return added;
    }
}