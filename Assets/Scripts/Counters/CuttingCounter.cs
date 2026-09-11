using UnityEngine;

public class CuttingCounter : ProcessingCounter<SliceableItemDefinition>, IInteractableAlternate
{
    private static readonly int Cut = Animator.StringToHash("Cut");

    [SerializeField] private SlicebleRecipeRepository repository;
    [SerializeField] private Animator animator;

    private CuttingProcess cuttingProcess = null;

    protected override bool IsTransferBlockedDuringProcessing => cuttingProcess != null && cuttingProcess.IsInProgress;
    protected override IRepository<KitchenItemDefinition, SliceableItemDefinition> Repository => repository;

    public bool TryInteractAlternateWith(IContainer container)
    {
        if (counterContainer.Item == null || cuttingProcess == null)
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
            return TryReplaceItemWith(cuttingProcess.Output);
        }

        return false;
    }

    protected override void StartProcessFrom(SliceableItemDefinition recipe)
    {
        cuttingProcess = new CuttingProcess(recipe);
        cuttingProcess.CutProgressChanged += HandleProgressChanged;
    }

    protected override void ResetProcess()
    {
        if (cuttingProcess != null)
        {
            cuttingProcess.CutProgressChanged -= HandleProgressChanged;
            cuttingProcess = null;
        }
    }
}