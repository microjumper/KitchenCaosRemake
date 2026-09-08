using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
    [SerializeField] private Transform anchor;
    [SerializeField] private StorePolicy storePolicy;

    private KitchenItem heldItem = null;

    public bool HasItem => heldItem != null;

    public bool CanStore(KitchenItem item)
    {
        if (heldItem == null)
        {
            return storePolicy == null || storePolicy.CanStore(item);
        }

        return false;
    }

    public bool TryRetrieve(out KitchenItem item)
    {
        if (heldItem == null)
        {
            item = null;

            return false;
        }

        item = heldItem;

        heldItem = null;

        return true;
    }

    // Try-pattern: atomically check and add, avoiding a TOCTOU race between Check() and Store().
    public bool TryStore(KitchenItem item)
    {
        if (CanStore(item))
        {
            heldItem = item;
            heldItem.transform.SetParent(anchor);
            heldItem.transform.SetPositionAndRotation(anchor.position, heldItem.transform.rotation);

            return true;
        }

        return false;
    }
}