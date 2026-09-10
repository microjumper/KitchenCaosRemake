public interface IContainer
{
    KitchenItem Item { get; }

    bool TryStore(KitchenItem item);
    bool TryRetrieve(out KitchenItem item);
}