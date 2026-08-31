using UnityEngine;

[RequireComponent(typeof(IContainer))]
public class TargetInteractor : MonoBehaviour
{
    [SerializeField] private TargetDetector targetDetector;
    
    private IContainer playerContainer;

    private IInteractable currentInteractable;

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
        if (target != null && target.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
        }
        else
        {
            currentInteractable = null;
        }
    }

    public void Interact()
    {
        currentInteractable?.TryInteractWith(playerContainer);
    }
}
