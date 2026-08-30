using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputProvider : MonoBehaviour, IInputProvider
{
    public Vector2 MoveInput { get; private set;  } = Vector2.zero;
    public event Action OnInteractPressed;

    public void OnMove(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Performed:
                MoveInput = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                MoveInput = Vector2.zero;
                break;
        }   
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            OnInteractPressed?.Invoke();
        }
    }
}
