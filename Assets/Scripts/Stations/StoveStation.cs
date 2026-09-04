using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class StoveStation : MonoBehaviour, IInteractable
{
    [SerializeField] private CookableRecipeRepository repository;

    private IContainer stationContainer;

    private void Awake()
    {
        stationContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.IsEmpty)
        {
            return StationTransfer.TryTransfer(stationContainer, otherContainer);
        }

        if (!stationContainer.IsEmpty)
        {
            Debug.Log("Station container is not empty. Cannot transfer items.");
            return false;
        }

        return TryTransferItemFrom(otherContainer);
    }

    private bool TryTransferItemFrom(IContainer otherContainer)
    {
        var heldObject = otherContainer.Peek();

        if (heldObject == null)
        {
            return false;
        }

        if (heldObject.TryGetComponent(out KitchenItem kitchenItem))
        {
            if (repository.TryGet(kitchenItem.Definition, out var recipe))
            {
                var transferred = StationTransfer.TryTransfer(otherContainer, stationContainer);

                if (transferred)
                {
                    // TODO Handle cooking logic
                }

                return transferred;
            }
        }

        return false;
    }
}
