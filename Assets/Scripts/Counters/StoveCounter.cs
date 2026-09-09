using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class StoveCounter : MonoBehaviour, IInteractable
{
    [SerializeField] private CookableRecipeRepository repository;
    [SerializeField] private ProgressBar progressBar;
    [SerializeField] private GameObject stoveOnEffects;

    private IContainer counterContainer;

    private CookingProcess cookingProcess = null;
    private bool IsProcessing => cookingProcess != null && !cookingProcess.IsComplete;

    protected virtual void Awake()
    {
        counterContainer = GetComponent<IContainer>();
    }

    private void Update()
    {
        if (cookingProcess == null)
        {
            return;
        }

        cookingProcess.AdvanceTime(Time.deltaTime);
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

    protected bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        var transferred = counterContainer.TryTransferTo(otherContainer);

        if (transferred)
        {
            ResetCookingProcess();

            DisableVisual();
        }

        return transferred;
    }

    private bool TryTransferStartingItemFrom(IContainer otherContainer)
    {
        if (repository.TryGet(otherContainer.HeldItem.Definition, out var recipe))
        {
            if (otherContainer.TryTransferTo(counterContainer))
            {
                StartCookingProcess(recipe);

                EnableVisual();

                return true;
            }
        }

        return false;
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
        if (counterContainer.TryRetrieve(out var item))
        {
            Destroy(item.gameObject);

            var processed = KitchenItemFactory.CreateFrom(itemDefinition);

            counterContainer.TryStore(processed);
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
