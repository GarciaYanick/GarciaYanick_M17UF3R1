using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLocomotion : MonoBehaviour
{
    CharacterMovement inputManager;
    AnimatorManager animatorManager;
    PlayerManager playerManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRB;

    [Header("Falling")]
    public float maxDistance = 0.5f;
    public float inAirTimer;
    public float leapingVelocity;
    public float fallingSpeed;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask groundLayer;

    [Header("MovementConditions")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    public bool isDancing;

    [Header("Speeds")]
    public float walkingSpeed = 1.5f;
    public float moveSpeed = 7;
    public float runningSpeed = 10;
    public float rotationSpeed = 15;

    [Header("Jump Speeds")]
    public float jumpHeight = 3;
    public float gravityIntensity = -15;

    public void Awake()
    {
        inputManager = GetComponent<CharacterMovement>();
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        playerRB = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting)
            return;
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
        if (isJumping)
            return;

        moveDirection = cameraObject.forward * inputManager.vertical;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * runningSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * moveSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }
        
        Vector3 movementVel = moveDirection;
        playerRB.velocity = movementVel;
    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDir = Vector3.zero;

        targetDir = cameraObject.forward * inputManager.vertical;
        targetDir = targetDir + cameraObject.right * inputManager.horizontal;
        targetDir.Normalize();
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    
        transform.rotation = playerRotation;
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        Debug.Log(isGrounded);
        if (!isGrounded && !isJumping)
        {
            Debug.Log(playerManager.isInteracting);
            if (!playerManager.isInteracting) 
            {
                animatorManager.PlayTargetAnim("fall", true);
                inAirTimer = inAirTimer + Time.deltaTime;
                playerRB.AddForce(transform.forward * leapingVelocity);
                playerRB.AddForce(-Vector3.up * fallingSpeed * inAirTimer);
            }
        }

        if (Physics.SphereCast(rayCastOrigin, 0.1f, Vector3.down, out hit, maxDistance, groundLayer))
        {
            Debug.Log("Me congraluta anunciar que entró a la condición amable damisela de compañía de precio razonable");
            if(!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnim("land", true);
            }
            inAirTimer = 0;
            isGrounded = true;
            playerManager.isInteracting = false;
        }
        else
        {
            isGrounded = false;
        }
    }

    public void HandleJumping()
    {
        if(isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnim("jump", false);

            float jumpingVel = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVel = moveDirection;
            playerVel.y = jumpingVel;
            playerRB.velocity = playerVel;
        }
    }
    public void HandleDancing()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnim("dance", false);
        }
    }
}
