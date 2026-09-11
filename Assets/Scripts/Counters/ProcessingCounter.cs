using UnityEngine;

[RequireComponent(typeof(IContainer))]
public abstract class ProcessingCounter<TRecipe> : MonoBehaviour, IInteractable
{
    [SerializeField] protected ProgressBar progressBar;

    protected IContainer counterContainer;

    protected virtual void Awake()
    {
        counterContainer = GetComponent<IContainer>();
    }

    public bool TryInteractWith(IContainer otherContainer)
    {
        if (IsTransferBlockedDuringProcessing)
        {
            return false;
        }

        if (otherContainer.Item == null || otherContainer.Item is IContainer)
            return TryTransferProcessedItemTo(otherContainer);

        if (counterContainer.Item == null && otherContainer.Item is IContainer)
            return false;

        if (counterContainer.Item != null)
        {
            return false;
        }

        return TryTransferStartingItemFrom(otherContainer);
    }

    protected abstract bool IsTransferBlockedDuringProcessing { get; }

    protected virtual bool TryTransferProcessedItemTo(IContainer otherContainer)
    {
        var transferred = counterContainer.TryTransferTo(otherContainer);

        if (transferred)
        {
            DisableUI();
            ResetProcess();
        }

        return transferred;
    }

    private bool TryTransferStartingItemFrom(IContainer otherContainer)
    {
        if (!Repository.TryGet(otherContainer.Item.Definition, out var recipe))
            return false;

        if (!otherContainer.TryTransferTo(counterContainer))
            return false;

        EnableUI();
        StartProcessFrom(recipe);

        return true;
    }

    protected abstract IRepository<KitchenItemDefinition, TRecipe> Repository { get; }

    protected abstract void ResetProcess();
    protected abstract void StartProcessFrom(TRecipe recipe);

    protected virtual void EnableUI()
    {
        progressBar.gameObject.SetActive(true);
    }

    protected virtual void DisableUI()
    {
        progressBar.gameObject.SetActive(false);
    }

    protected void HandleProgressChanged(float progress)
    {
        progressBar.SetProgress(progress);
    }

    protected bool TryReplaceItemWith(KitchenItemDefinition itemDefinition)
    {
        if (!counterContainer.TryRetrieve(out var oldItem))
        {
            return false;
        }

        Destroy(oldItem.gameObject);

        var newItem = KitchenItemFactory.CreateFrom(itemDefinition);

        if (counterContainer.TryStore(newItem))
        {
            return true;
        }

        Destroy(newItem.gameObject);

        return false;
    }
}
