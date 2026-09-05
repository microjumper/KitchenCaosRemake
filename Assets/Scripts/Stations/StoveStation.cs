using UnityEngine;

public class StoveStation : ProcessingStation
{
    [SerializeField] private CookableRecipeRepository repository;
    [SerializeField] private GameObject stoveOnEffects;

    protected override bool IsProcessing => cookingProcess != null && !cookingProcess.IsComplete;

    private CookingProcess cookingProcess = null;

    protected override bool TryStartProcessing(KitchenItemDefinition startingItemDefinition)
    {
        if (!repository.TryGet(startingItemDefinition, out var recipe))
        {
            return false;
        }

        cookingProcess = new CookingProcess(recipe);
        cookingProcess.ItemCooked += OnItemCooked;
        cookingProcess.ItemBurned += OnItemBurned;

        stoveOnEffects.SetActive(true);

        return true;
    }

    protected override bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        var transferred = base.TryTransferProcessedItemTo(otherContainer);

        if (transferred)
        {
            if (cookingProcess != null)
            {
                cookingProcess.ItemCooked -= OnItemCooked;
                cookingProcess.ItemBurned -= OnItemBurned;
                cookingProcess = null;

                stoveOnEffects.SetActive(false);
            }
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
    }

    private void OnItemBurned()
    {
        ReplaceWith(cookingProcess.Burned);

        cookingProcess.ItemBurned -= OnItemBurned;
        cookingProcess = null;

        stoveOnEffects.SetActive(false);
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
}
