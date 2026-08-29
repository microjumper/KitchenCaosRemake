using UnityEngine;

public class TargetInteractor : MonoBehaviour
{
    [SerializeField] private TargetDetector targetDetector;

    private IInteractable currentInteractable;

    private void Awake()
    {
        if (targetDetector != null)
        {
            targetDetector.OnTargetChange += OnTargetInteraction;
        }
    }

    private void OnDestroy()
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
}
