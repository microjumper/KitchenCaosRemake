using UnityEngine;

public class PlayerInteractor : MonoBehaviour, IInteractor
{
    [SerializeField] private LayerMask targetableMask;
    [SerializeField] private float castDistance = 1.0f;

    private TargetFacade currentTarget;

    private void FixedUpdate()
    {
        FindTarget();
    }

    private void FindTarget()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, castDistance, targetableMask))
        {
            // The hit object has a TargetFacade component because of the layer mask
            var target = hit.collider.GetComponent<TargetFacade>();

            SelectTarget(target);
        }
        else
        {
            SelectTarget(null);
        }
    }

    private void SelectTarget(TargetFacade target)
    {
        if (target == currentTarget)
        {
            return;
        }

        if (currentTarget != null)
        {
            currentTarget.Selectable?.Deselect();
        }

        currentTarget = target;

        if (currentTarget != null)
        {
            currentTarget.Selectable?.Select();
        }
    }
}