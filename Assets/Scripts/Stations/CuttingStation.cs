using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class CuttingStation : MonoBehaviour, IInteractable
{
    private static readonly int Cut = Animator.StringToHash("Cut");

    [SerializeField] private SlicingRecipeRepository repository;
    [SerializeField] private Animator animator;

    private IContainer stationContainer;

    private void Awake()
    {
        stationContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (otherContainer.IsEmpty)
        {
            return StationTransferer.TryTransfer(stationContainer, otherContainer);
        }

        if (!stationContainer.IsEmpty)
        {
            return false;
        }

        var heldObject = otherContainer.Peek();

        if (heldObject == null)
        {
            return false;
        }

        if (heldObject.TryGetComponent(out KitchenItem kitchenItem))
        {
            if (repository.TryGet(kitchenItem.Definition, out _))
            {
                return StationTransferer.TryTransfer(otherContainer, stationContainer);
            }
        }

        return false;
    }

    public void Slice()
    {
        animator.SetTrigger(Cut);
    }
}
