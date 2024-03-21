using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    NinjaController input;

    public Vector2 moveInput;
    public float vertical;
    public float horizontal;

    //Enables inputs
    private void OnEnable()
    {
        if(input == null)
        {
            input = new NinjaController();

            input.MoveControl.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        }

        input.Enable();
    }

    //Disables inputs
    private void OnDisable()
    {
        input.Disable();
    }

    private void HandleMovementInput()
    {
        vertical = moveInput.y;
        horizontal = moveInput.x;
    }
}
