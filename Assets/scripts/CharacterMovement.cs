using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    NinjaController input;
    PlayerLocomotion playerLocomotion;
    AnimatorManager animatorManager;

    public Vector2 moveInput;

    public float moveAmount;
    public float vertical;
    public float horizontal;

    public bool shift_input;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    //Enables inputs
    private void OnEnable()
    {
        if(input == null)
        {
            input = new NinjaController();

            input.MoveControl.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
            input.PlayerActions.Actions.performed += ctx => shift_input = true;
            input.PlayerActions.Actions.canceled += ctx => shift_input = false;
        }

        input.Enable();
    }

    //Disables inputs
    private void OnDisable()
    {
        input.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleSprintingInput();
        //HandleJumpingInput();
        //HandleDanceInput();
    }

    private void HandleMovementInput()
    {
        vertical = moveInput.y;
        horizontal = moveInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerLocomotion.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (shift_input && moveAmount > 0.5f)
        {
            playerLocomotion.isSprinting = true;
        }
        else
        {
            playerLocomotion.isSprinting = false;
        }
    }
}
