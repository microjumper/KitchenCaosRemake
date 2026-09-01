using UnityEngine;

public sealed class SingleSlotContainer : MonoBehaviour, IContainer
{
    [SerializeField] private Transform anchor;

    private GameObject heldItem = null;

    public GameObject Peek() => heldItem;

    // Try-pattern: atomically check and add, avoiding a TOCTOU race between Check() and Add().
    public bool TryAdd(GameObject item)
    {
        if (IsEmpty)
        {
            heldItem = item;
            heldItem.transform.SetPositionAndRotation(anchor.position, anchor.rotation);
            heldItem.transform.SetParent(anchor);
            
            return true;
        }

        return false;
    }

    public bool TryRemove(out GameObject item)
    {
        if (IsEmpty)
        {
            item = null;

            return false;
        }

        item = heldItem;
        item.transform.SetParent(null);

        heldItem = null;

        return true;
    }

    public bool IsEmpty => heldItem == null;
}
