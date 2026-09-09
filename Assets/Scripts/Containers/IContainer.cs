public interface IContainer
{
    KitchenItem HeldItem { get; }

    bool TryStore(KitchenItem item);
    bool TryRetrieve(out KitchenItem item);
}