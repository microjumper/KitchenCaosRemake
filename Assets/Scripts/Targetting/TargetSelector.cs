using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private TargetRaycaster targetCaster;

    private ISelectable currentSelectable;

    private void Awake()
    {
        if (targetCaster != null)
        {
            targetCaster.OnTargetFound += OnTargetSelection;
        }
    }

    private void OnDestroy()
    {
        if (targetCaster != null)
        {
            targetCaster.OnTargetFound -= OnTargetSelection;
        }
    }

    private void OnTargetSelection(RaycastHit? hit)
    {
        if (hit.HasValue && hit.Value.collider.TryGetComponent<ISelectable>(out var selectable))
        {
            if (currentSelectable != null && currentSelectable != selectable)
            {
                currentSelectable.Deselect();
            }

            currentSelectable = selectable;

            currentSelectable.Select();
        }
        else
        {
            currentSelectable?.Deselect();

            currentSelectable = null;
        }
    }
}
