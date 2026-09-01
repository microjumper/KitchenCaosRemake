using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class TargetInteractor : MonoBehaviour
{
    [SerializeField] private TargetDetector targetDetector;

    private IContainer playerContainer;

    private GameObject currentTarget;

    private void Awake()
    {
        playerContainer = GetComponent<IContainer>();
    }

    private void OnEnable()
    {
        if (targetDetector != null)
        {
            targetDetector.OnTargetChange += OnTargetInteraction;
        }
    }

    private void OnDisable()
    {
        if (targetDetector != null)
        {
            targetDetector.OnTargetChange -= OnTargetInteraction;
        }
    }

    private void OnTargetInteraction(GameObject target)
    {
        currentTarget = target;
    }

    public void Interact()
    {
        if (currentTarget != null && currentTarget.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.TryInteractWith(playerContainer);
        }
    }

    public void InteractAlternate()
    {
        if (currentTarget != null && currentTarget.TryGetComponent<IInteractableAlternate>(out var interactableAlternate))
        {
            interactableAlternate.TryInteractAlternateWith(playerContainer);
        }
    }
}
