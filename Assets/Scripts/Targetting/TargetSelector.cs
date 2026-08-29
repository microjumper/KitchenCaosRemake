using UnityEngine;

public class TargetSelector : MonoBehaviour
{
    [SerializeField] private TargetDetector targetDetector;

    private ISelectable currentSelectable;

    private void Awake()
    {
        if (targetDetector != null)
        {
            targetDetector.OnTargetChange += OnTargetSelection;
        }
    }

    private void OnDestroy()
    {
        if (targetDetector != null)
        {
            targetDetector.OnTargetChange -= OnTargetSelection;
        }
    }

    private void OnTargetSelection(GameObject target)
    {
        if (target != null && target.TryGetComponent<ISelectable>(out var selectable))
        {
            if (currentSelectable == selectable)
            {
                return;
            }

            currentSelectable?.Deselect();
            currentSelectable = selectable;
            currentSelectable?.Select();
        }
        else
        {
            currentSelectable?.Deselect();

            currentSelectable = null;
        }
    }
}
