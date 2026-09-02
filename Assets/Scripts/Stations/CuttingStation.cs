using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class CuttingStation : MonoBehaviour, IInteractable, IInteractableAlternate
{
    private static readonly int Cut = Animator.StringToHash("Cut");

    [SerializeField] private SlicebleRecipeRepository repository;
    [SerializeField] private Animator animator;

    private IContainer stationContainer;
    private CuttingProcess cuttingProcess = null;

    private void Awake()
    {
        stationContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (IsCuttingInProgress())
        {
            Debug.Log("Cutting in progress. Cannot transfer items.");
            return false;
        }

        if (otherContainer.IsEmpty)
        {
            return TryTransferItemTo(otherContainer);
        }

        if (!stationContainer.IsEmpty)
        {
            Debug.Log("Station container is not empty. Cannot transfer items.");
            return false;
        }

        return TryTransferItemFrom(otherContainer);
    }

    public bool TryInteractAlternateWith(IContainer container)
    {
        if (stationContainer.IsEmpty)
        {
            return false;
        }

        if (!cuttingProcess.IsComplete)
        {
            cuttingProcess.Cut();
            animator.SetTrigger(Cut);
        }

        if (cuttingProcess.IsComplete)
        {
            if (stationContainer.TryRemove(out var whole))
            {
                Destroy(whole);

                var sliced = KitchenItemFactory.CreateFrom(cuttingProcess.Output);

                stationContainer.TryAdd(sliced.gameObject);

                return true;
            }

            return false;
        }

        return true;
    }

    private bool IsCuttingInProgress() => cuttingProcess != null && cuttingProcess.IsInProgress;

    private bool TryTransferItemTo(IContainer otherContainer)
    {
        var transferred = StationTransferer.TryTransfer(stationContainer, otherContainer);

        if (transferred)
        {
            cuttingProcess = null;
        }

        return transferred;
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
                var transferred = StationTransferer.TryTransfer(otherContainer, stationContainer);

                if (transferred)
                {
                    cuttingProcess = new(recipe);
                }

                return transferred;
            }
        }

        return false;
    }
}
