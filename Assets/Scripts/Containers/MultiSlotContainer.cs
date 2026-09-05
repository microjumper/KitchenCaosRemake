using System.Collections.Generic;
using UnityEngine;

public sealed class MultiSlotContainer : MonoBehaviour, IContainer
{
    private const float ItemOffset = 0.10f;
    [SerializeField] private Transform anchor;

    private readonly Stack<GameObject> heldItems = new ();

    public bool IsEmpty => heldItems.Count == 0;
    public int Count => heldItems.Count;

    public GameObject Peek() => IsEmpty ? null : heldItems.Peek();

    public bool TryAdd(GameObject item)
    {
        item.transform.SetPositionAndRotation(anchor.position + heldItems.Count * ItemOffset * Vector3.up, item.transform.rotation);
        item.transform.SetParent(anchor);

        heldItems.Push(item);

        return true;
    }

    public bool TryRemove(out GameObject item)
    {
        if (IsEmpty)
        {
            item = null;

            return false;
        }

        item = heldItems.Pop();

        return true;
    }
}