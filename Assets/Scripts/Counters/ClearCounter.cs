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
        if (otherContainer.HeldItem == null)
        {
            return counterContainer.TryTransferTo(otherContainer);
        }

        if (counterContainer.HeldItem == null)
        {
            return otherContainer.TryTransferTo(counterContainer);
        }

        return false;
    }
}
