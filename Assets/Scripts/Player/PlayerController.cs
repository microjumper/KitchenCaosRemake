using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidbody;
    private IInputProvider inputProvider;
    private PlayerAnimator playerAnimator;

    [SerializeField] private float moveSpeed = 5f;


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        inputProvider = GetComponentInChildren<IInputProvider>();
        playerAnimator = GetComponentInChildren<PlayerAnimator>();
    }

    private void FixedUpdate()
    {
        Move();
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
}
