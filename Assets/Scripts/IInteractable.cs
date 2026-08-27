public interface IInteractable
{
    bool CanInteract { get; }
    bool TryInteract();
}