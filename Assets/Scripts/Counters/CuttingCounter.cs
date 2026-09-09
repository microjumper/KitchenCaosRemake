using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class CuttingCounter : MonoBehaviour, IInteractable, IInteractableAlternate
{
    private static readonly int Cut = Animator.StringToHash("Cut");

    [SerializeField] private SlicebleRecipeRepository repository;
    [SerializeField] private Animator animator;
    [SerializeField] private ProgressBar progressBar;

    private IContainer counterContainer;

    private CuttingProcess cuttingProcess = null;
    private bool IsProcessing => cuttingProcess != null && cuttingProcess.IsInProgress;


    private void Awake()
    {
        counterContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (IsProcessing)
        {
            Debug.Log($"{GetType().Name} is processing. Cannot transfer items.");

            return false;
        }

        if (otherContainer.HeldItem == null)
        {
            return TryTransferProcessedItemTo(otherContainer);
        }

        if (counterContainer.HeldItem != null)
        {
            Debug.Log("Counter container is not empty. Cannot transfer items.");

            return false;
        }

        return TryTransferStartingItemFrom(otherContainer);
    }

    public bool TryInteractAlternateWith(IContainer container)
    {
        if (counterContainer.HeldItem == null || cuttingProcess == null)
        {
            return false;
        }

        if (!cuttingProcess.IsComplete)
        {
            cuttingProcess.Cut();
            animator.SetTrigger(Cut);
        }

        if (cuttingProcess.IsComplete && counterContainer.TryRetrieve(out var whole))
        {
            Destroy(whole.gameObject);

            var sliced = KitchenItemFactory.CreateFrom(cuttingProcess.Output);

            if (counterContainer.TryStore(sliced))
            {
                return true;
            }

            Destroy(sliced.gameObject);
        }

        return false;
    }

    private bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        var transferred = counterContainer.TryTransferTo(otherContainer);

        if (transferred)
        {
            ResetCuttingProcess();
        }

        return transferred;
    }

    private bool TryTransferStartingItemFrom(IContainer otherContainer)
    {
        if (repository.TryGet(otherContainer.HeldItem.Definition, out var recipe))
        {
            if (otherContainer.TryTransferTo(counterContainer))
            {
                cuttingProcess = new CuttingProcess(recipe);
                cuttingProcess.CutProgressChanged += HandleCutProgressChanged;

                progressBar.gameObject.SetActive(true);

                return true;
            }
        }

        return false;
    }

    private void ResetCuttingProcess()
    {
        if (cuttingProcess != null)
        {
            cuttingProcess.CutProgressChanged -= HandleCutProgressChanged;
            cuttingProcess = null;
        }

        progressBar.gameObject.SetActive(false);
    }

    private void HandleCutProgressChanged(float progress) => progressBar.SetProgress(progress);
}