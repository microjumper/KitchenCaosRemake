using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerAnimator playerAnimator;
    [SerializeField] private PlayerInputProvider inputProvider;
    [SerializeField] private TargetInteractor interactor;

    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        inputProvider.InteractPressed += HandleInteraction;
        inputProvider.InteractAlternatePressed += HandleInteractionAlternate;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void OnDisable()
    {
        inputProvider.InteractPressed -= HandleInteraction;
        inputProvider.InteractAlternatePressed -= HandleInteractionAlternate;
    }

    private void Move()
    {
        Vector3 direction = new(inputProvider.MoveInput.x, 0f, inputProvider.MoveInput.y);
        Vector3 destination = transform.position + moveSpeed * Time.fixedDeltaTime * direction;

        if (direction.sqrMagnitude > 0.01f)
        {
            rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }

        rigidbody.MovePosition(destination);

        playerAnimator.SetWalking(direction);
    }

    private void HandleInteraction() => interactor.Interact();

    private void HandleInteractionAlternate() => interactor.InteractAlternate();
}
