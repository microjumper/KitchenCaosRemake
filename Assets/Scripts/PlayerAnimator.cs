using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    public static readonly int IsWalking = Animator.StringToHash("IsWalking");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetWalking(Vector3 movementInput)
    {
        animator.SetBool(IsWalking, movementInput.sqrMagnitude > 0.01f);
    }
}
