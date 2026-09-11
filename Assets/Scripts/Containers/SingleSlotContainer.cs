using UnityEngine;

public class Container : MonoBehaviour, IContainer
{
    [SerializeField] private Transform anchor;

    public KitchenItem Item { get; private set; } = null;

    public bool TryRetrieve(out KitchenItem item)
    {
        if (Item == null)
        {
            item = null;

            return false;
        }

        item = Item;

        Item = null;

        return true;
    }

    // Try-pattern: atomically check and add, avoiding a TOCTOU race between Check() and Store().
    public bool TryStore(KitchenItem item)
    {
        if (Item == null)
        {
            Item = item;
            Item.transform.SetParent(anchor);
            Item.transform.SetPositionAndRotation(anchor.position, Item.transform.rotation);

            return true;
        }

        if (Item is Plate plate)
        {
            return plate.TryAdd(item);
        }

        return false;
    }
}