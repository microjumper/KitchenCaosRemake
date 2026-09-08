using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class ClearCounter : MonoBehaviour, IInteractable
{
    private IContainer counterContainer;

    private void Awake()
    {
        counterContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.HasItem)
        {
            return ItemTransfer.TryTransfer(otherContainer, counterContainer);
        }

        if (counterContainer.HasItem)
        {
            return ItemTransfer.TryTransfer(counterContainer, otherContainer);
        }

        return false;
    }
}
