using UnityEngine;

[RequireComponent(typeof(IContainer))]
public abstract class ProcessingCounter : MonoBehaviour, IInteractable
{
    protected IContainer CounterContainer { get; private set; }

    protected virtual void Awake()
    {
        CounterContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (IsProcessing)
        {
            Debug.Log($"{GetType().Name} is processing. Cannot transfer items.");
            
            return false;
        }

        if (!otherContainer.HasItem)
        {
            return TryTransferProcessedItemTo(otherContainer);
        }

        if (CounterContainer.HasItem)
        {
            Debug.Log("Counter container is not empty. Cannot transfer items.");
            
            return false;
        }

        return TryTransferStartingItemFrom(otherContainer);
    }

    protected abstract bool IsProcessing { get; }

    protected virtual bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        return ItemTransfer.TryTransfer(CounterContainer, otherContainer);
    }

    private bool TryTransferStartingItemFrom(IContainer otherContainer)
    {
        if (otherContainer.TryRetrieve(out KitchenItem kitchenItem))
        {
            if (TryStartProcessing(kitchenItem.Definition))
            {
                return CounterContainer.TryStore(kitchenItem);
            }
        }

        return false;
    }

    protected abstract bool TryStartProcessing(KitchenItemDefinition startingItemDefinition);
}