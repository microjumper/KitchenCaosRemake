using System.Collections.Generic;
using UnityEngine;

public class StackedItemContainer : MonoBehaviour, IContainer
{
    [SerializeField] private Transform anchor;
    [SerializeField] private StorePolicy storePolicy;

    private const float ItemOffset = 0.10f;

    private readonly Stack<KitchenItem> heldItems = new();

    public bool HasItem => heldItems.Count > 0;

    public bool CanStore(KitchenItem item)
    {
        return storePolicy == null || storePolicy.CanStore(item);
    }

    public bool TryRetrieve(out KitchenItem item)
    {
        if (heldItems.Count == 0)
        {
            item = null;

            return false;
        }

        item = heldItems.Pop();
        item.transform.SetParent(null);

        return true;
    }

    public bool TryStore(KitchenItem item)
    {
        if (CanStore(item))
        {
            item.transform.SetParent(anchor);
            var position = anchor.position + heldItems.Count * ItemOffset * Vector3.up;
            item.transform.SetPositionAndRotation(position, item.transform.rotation);

            heldItems.Push(item);

            return true;
        }

        return false;
    }
}