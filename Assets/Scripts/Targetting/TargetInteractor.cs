using UnityEngine;

public class TargetInteractor : MonoBehaviour
{
    [SerializeField] private TargetRaycaster targetCaster;

    private IInteractable currentInteractable;

    private void Awake()
    {
        if (targetCaster != null)
        {
            targetCaster.OnTargetChange += OnTargetInteraction;
        }
    }

    private void OnDestroy()
    {
        if (targetCaster != null)
        {
            targetCaster.OnTargetChange -= OnTargetInteraction;
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
