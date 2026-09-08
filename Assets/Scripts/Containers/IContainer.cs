public interface IContainer
{
    bool HasItem { get; }
    bool CanStore(KitchenItem item);
    bool TryStore(KitchenItem item);
    bool TryRetrieve(out KitchenItem item);
}