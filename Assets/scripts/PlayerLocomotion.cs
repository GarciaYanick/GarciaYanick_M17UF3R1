using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLocomotion : MonoBehaviour
{
    CharacterMovement inputManager;
    PlayerManager playerManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRB;

    public bool isSprinting;
    public bool isGrounded;

    [Header("Speeds")]
    public float walkingSpeed = 1.5f;
    public float moveSpeed = 7;
    public float runningSpeed = 10;
    public float rotationSpeed = 15;

    public void Awake()
    {
        inputManager = GetComponent<CharacterMovement>();
        playerManager = GetComponent<PlayerManager>();
        playerRB = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
    }

    private void HandleMovement()
    {
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
}
