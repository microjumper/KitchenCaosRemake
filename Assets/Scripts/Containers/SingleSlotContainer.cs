using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
    [SerializeField] private Transform anchor;

    public KitchenItem HeldItem { get; private set; } = null;

    public bool TryRetrieve(out KitchenItem item)
    {
        if (HeldItem == null)
        {
            item = null;

            return false;
        }

        item = HeldItem;

        HeldItem = null;

        return true;
    }

    // Try-pattern: atomically check and add, avoiding a TOCTOU race between Check() and Store().
    public bool TryStore(KitchenItem item)
    {
        if (HeldItem == null)
        {
            HeldItem = item;
            HeldItem.transform.SetParent(anchor);
            HeldItem.transform.SetPositionAndRotation(anchor.position, HeldItem.transform.rotation);

            return true;
        }

        return false;
    }
}