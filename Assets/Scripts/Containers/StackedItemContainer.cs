using System.Collections.Generic;
using UnityEngine;

public class StackedItemContainer : MonoBehaviour, IContainer
{
    [SerializeField] private Transform anchor;

    private const float ItemOffset = 0.10f;

    private readonly Stack<KitchenItem> itemStack = new();

    public KitchenItem Item => itemStack.Peek();

    public bool TryRetrieve(out KitchenItem item)
    {
        if (itemStack.Count == 0)
        {
            item = null;

            return false;
        }

        item = itemStack.Pop();
        item.transform.SetParent(null);

        return true;
    }

    public bool TryStore(KitchenItem item)
    {
        item.transform.SetParent(anchor);
        var position = anchor.position + itemStack.Count * ItemOffset * Vector3.up;
        item.transform.SetPositionAndRotation(position, item.transform.rotation);

        itemStack.Push(item);

        return true;
    }
}