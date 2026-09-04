using UnityEngine;

public sealed class CuttingStation : ProcessingStation, IInteractableAlternate
{
    private static readonly int Cut = Animator.StringToHash("Cut");

    [SerializeField] private SlicebleRecipeRepository repository;
    [SerializeField] private Animator animator;
    [SerializeField] private ProgressBar progressBar;

    private CuttingProcess cuttingProcess = null;

    protected override bool IsProcessing => cuttingProcess != null && cuttingProcess.IsInProgress;

    protected override bool TryStartProcessing(KitchenItemDefinition startingItemDefinition)
    {
        if (!repository.TryGet(startingItemDefinition, out var recipe))
        {
            return false;
        }

        cuttingProcess = new CuttingProcess(recipe);
        cuttingProcess.CutProgressChanged += HandleCutProgressChanged;

        progressBar.gameObject.SetActive(true);

        return true;
    }

    protected override bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        var transferred = base.TryTransferProcessedItemTo(otherContainer);

        if (transferred)
        {
            ResetCuttingProcess();
        }

        return transferred;
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

    public bool TryInteractAlternateWith(IContainer container)
    {
        if (StationContainer.IsEmpty || cuttingProcess == null)
        {
            return false;
        }

        if (!cuttingProcess.IsComplete)
        {
            cuttingProcess.Cut();
            animator.SetTrigger(Cut);
        }

        if (!cuttingProcess.IsComplete)
        {
            return true;
        }

        if (!StationContainer.TryRemove(out var whole))
        {
            return false;
        }

        Destroy(whole);

        var sliced = KitchenItemFactory.CreateFrom(cuttingProcess.Output);

        if (!StationContainer.TryAdd(sliced.gameObject))
        {
            Destroy(sliced.gameObject);
            return false;
        }

        return true;
    }
}