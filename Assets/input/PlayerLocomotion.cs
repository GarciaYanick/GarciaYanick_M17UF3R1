using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    CharacterMovement inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRB;

    public float moveSpeed = 7;
    public float rotationSpeed = 15;

    public void Awake()
    {
        inputManager = GetComponent<CharacterMovement>();
        playerRB = GetComponent<Rigidbody>();
    }
    public void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.vertical;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontal;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * moveSpeed;

        Vector3 movementVel = moveDirection;
        playerRB.velocity = movementVel;
    }

    public void HandleRotation()
    {
        Vector3 targetDir = Vector3.zero;

        targetDir = cameraObject.forward * inputManager.vertical;
        targetDir = targetDir + cameraObject.right * inputManager.horizontal;
        targetDir.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(targetDir);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    
        transform.rotation = playerRotation;
    }
}
