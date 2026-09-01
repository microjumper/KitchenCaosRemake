
using System;
using UnityEngine;

public interface IInputProvider
{
    Vector2 MoveInput { get; }
    event Action InteractPressed;
    event Action InteractAlternatePressed;
}