using System;
using UnityEngine;

public class TargetRaycaster : MonoBehaviour
{
    public Action<RaycastHit?> OnTargetFound;

    [SerializeField] private LayerMask targetableMask;
    [SerializeField] private float castDistance = 1.0f;

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, castDistance, targetableMask))
        {
            OnTargetFound?.Invoke(hit);
        }
        else
        {
            OnTargetFound?.Invoke(null);
        }
    }
}
