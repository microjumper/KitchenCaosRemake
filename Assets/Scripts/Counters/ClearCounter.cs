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
        if (counterContainer.Item != null && counterContainer.TryTransferTo(otherContainer))
        {
            return true;
        }

        if (otherContainer.Item != null && otherContainer.TryTransferTo(counterContainer))
        {
            return true;
        }

        return false;
    }
}
