using UnityEngine;

public class Plate : KitchenItem, IContainer
{
    [SerializeField] private Deliverable deliverable;

    public KitchenItem Item { get; private set; }

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

    public bool TryStore(KitchenItem item)
    {
        var delivered = deliverable.TryAdd(item.Definition);

        if (delivered)
        {
            Destroy(item.gameObject);
        }

        return delivered;
    }
}