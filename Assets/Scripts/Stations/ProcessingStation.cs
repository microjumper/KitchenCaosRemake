using UnityEngine;

[RequireComponent(typeof(IContainer))]
public abstract class ProcessingStation : MonoBehaviour, IInteractable
{
    protected IContainer StationContainer { get; private set; }

    protected virtual void Awake()
    {
        StationContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (IsProcessing)
        {
            Debug.Log($"{GetType().Name} is processing. Cannot transfer items.");
            
            return false;
        }

        if (otherContainer.IsEmpty)
        {
            return TryTransferProcessedItemTo(otherContainer);
        }

        if (!StationContainer.IsEmpty)
        {
            Debug.Log("Station container is not empty. Cannot transfer items.");
            
            return false;
        }

        return TryTransferStartingItemFrom(otherContainer);
    }

    protected abstract bool IsProcessing { get; }

    protected virtual bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        return StationTransfer.TryTransfer(StationContainer, otherContainer);
    }

    private bool TryTransferStartingItemFrom(IContainer otherContainer)
    {
        var startingItem = otherContainer.Peek();

        if (startingItem == null || !startingItem.TryGetComponent(out KitchenItem kitchenItem))
        {
            return false;
        }

        if (!TryStartProcessing(kitchenItem.Definition))
        {
            return false;
        }

        return StationTransfer.TryTransfer(otherContainer, StationContainer);
    }

    protected abstract bool TryStartProcessing(KitchenItemDefinition startingItemDefinition);
}