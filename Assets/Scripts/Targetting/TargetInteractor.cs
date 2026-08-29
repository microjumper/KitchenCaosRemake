using UnityEngine;

public class TargetInteractor : MonoBehaviour
{
    [SerializeField] private TargetRaycaster targetCaster;

    private IInteractable currentInteractable;

    private void Awake()
    {
        if (targetCaster != null)
        {
            targetCaster.OnTargetFound += OnTargetInteraction;
        }
    }

    private void OnDestroy()
    {
        if (targetCaster != null)
        {
            targetCaster.OnTargetFound -= OnTargetInteraction;
        }
    }

    private void OnTargetInteraction(RaycastHit? hit)
    {
        if (hit.HasValue && hit.Value.collider.TryGetComponent<IInteractable>(out var interactable))
        {
            currentInteractable = interactable;
        }
        else
        {
            currentInteractable = null;
        }
    }
}
