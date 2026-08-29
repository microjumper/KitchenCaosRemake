public interface IInteractable
{
    bool CanInteractWith(IInteractor interactor);
    bool InteractWith(IInteractor interactor);
}