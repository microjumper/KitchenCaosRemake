using UnityEngine;

public class StoveCounter : ProcessingCounter<CookableItemDefinition>
{
    [SerializeField] private CookableRecipeRepository repository;
    [SerializeField] private GameObject stoveOnEffects;

    private CookingProcess cookingProcess = null;

    protected override bool IsTransferBlockedDuringProcessing => cookingProcess != null && !cookingProcess.IsComplete;
    protected override IRepository<KitchenItemDefinition, CookableItemDefinition> Repository => repository;

    private void Update()
    {
        if (cookingProcess == null)
        {
            return;
        }

        cookingProcess.AdvanceTime(Time.deltaTime);
    }

    protected override void StartProcessFrom(CookableItemDefinition recipe)
    {
        cookingProcess = new CookingProcess(recipe);
        cookingProcess.ItemCooked += OnItemCooked;
        cookingProcess.ItemBurned += OnItemBurned;

        cookingProcess.CookingProgressChanged += HandleProgressChanged;
    }

    protected override void ResetProcess()
    {
        if (cookingProcess != null)
        {
            cookingProcess.ItemCooked -= OnItemCooked;
            cookingProcess.ItemBurned -= OnItemBurned;

            cookingProcess.CookingProgressChanged -= HandleProgressChanged;

            cookingProcess = null;
        }
    }

    protected override void EnableVisual()
    {
        base.EnableVisual();

        stoveOnEffects.SetActive(true);
    }

    protected override void DisableVisual()
    {
        base.DisableVisual();

        stoveOnEffects.SetActive(false);
    }


    private void OnItemCooked()
    {
        TryReplaceItemWith(cookingProcess.Cooked);

        cookingProcess.ItemCooked -= OnItemCooked;

        progressBar.UseSecondaryColor();
    }

    private void OnItemBurned()
    {
        TryReplaceItemWith(cookingProcess.Burned);

        ResetProcess();

        DisableVisual();
    }
}
