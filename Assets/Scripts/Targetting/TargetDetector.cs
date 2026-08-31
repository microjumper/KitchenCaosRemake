using System;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    public event Action<GameObject> OnTargetChange;

    [SerializeField] private LayerMask targetableMask;
    [SerializeField] private float castDistance = 1.0f;

    private GameObject currentTarget = null;

    private void FixedUpdate()
    {
        GameObject target = null;
        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, castDistance, targetableMask))
        {
            target = hit.collider.gameObject;
        }

        if (target == currentTarget)
        {
            return;
        }

        currentTarget = target; 
        
        OnTargetChange?.Invoke(currentTarget);
    }
}
