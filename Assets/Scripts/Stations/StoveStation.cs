using UnityEngine;

public class StoveStation : ProcessingStation
{
    [SerializeField] private CookableRecipeRepository repository;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private GameObject stoveOnEffects;

    protected override bool IsProcessing => cookingProcess != null && !cookingProcess.IsComplete;

    private CookingProcess cookingProcess = null;

    protected override bool TryStartProcessing(KitchenItemDefinition startingItemDefinition)
    {
        if (!repository.TryGet(startingItemDefinition, out var recipe))
        {
            return false;
        }

        StartCookingProcess(recipe);

        EnableVisual();

        return true;
    }

    protected override bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        var transferred = base.TryTransferProcessedItemTo(otherContainer);

        if (transferred)
        {
            ResetCookingProcess();

            DisableVisual();
        }

        return transferred;
    }

    private void Update()
    {
        if (cookingProcess == null)
        {
            return;
        }

        cookingProcess.AdvanceTime(Time.deltaTime);
    }

    private void OnItemCooked()
    {
        ReplaceWith(cookingProcess.Cooked);

        cookingProcess.ItemCooked -= OnItemCooked;

        progressBar.UseSecondaryColor();
    }

    private void OnItemBurned()
    {
        ReplaceWith(cookingProcess.Burned);

        ResetCookingProcess();

        DisableVisual();
    }

    private void ReplaceWith(KitchenItemDefinition itemDefinition)
    {
        if (StationContainer.TryRemove(out var item))
        {
            Destroy(item);

            var processed = KitchenItemFactory.CreateFrom(itemDefinition);

            StationContainer.TryAdd(processed.gameObject);
        }
    }

    private void StartCookingProcess(CookableItemDefinition recipe)
    {
        cookingProcess = new CookingProcess(recipe);
        cookingProcess.ItemCooked += OnItemCooked;
        cookingProcess.ItemBurned += OnItemBurned;

        cookingProcess.CookingProgressChanged += HandleProgressChanged;
    }

    private void ResetCookingProcess()
    {
        if (cookingProcess != null)
        {
            cookingProcess.ItemCooked -= OnItemCooked;
            cookingProcess.ItemBurned -= OnItemBurned;
            cookingProcess.CookingProgressChanged -= HandleProgressChanged;
            cookingProcess = null;
        }
    }

    private void EnableVisual()
    {
        stoveOnEffects.SetActive(true);
        progressBar.gameObject.SetActive(true);
    }

    private void DisableVisual()
    {
        stoveOnEffects.SetActive(false);
        progressBar.gameObject.SetActive(false);
    }

    private void HandleProgressChanged(float progress) => progressBar.SetProgress(progress);
}
