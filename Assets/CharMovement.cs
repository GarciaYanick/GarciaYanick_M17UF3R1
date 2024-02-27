using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Animator playerAnimation;
    private CharacterController playerCharController;

    public float speed = 5f;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private Vector3 velocity;
    private bool isGrounded;

    //// Add boolean parameters for animation control
    //private bool isDead;
    //private bool isRunning;
    //private bool isJumping;
    //private bool isWalking;

    void Awake()
    {
       playerInput = GetComponent<PlayerInput>();
        playerAnimation = GetComponent<Animator>();
        playerCharController = GetComponent<CharacterController>();

        if (playerAnimation == null)
        {
            Debug.LogError("No hay nada que animar en el objeto del juego, metele algo koooooooonyo.");
        }
    }

    void Start()
    {
       playerInput.actions["Dance"].performed += ctx => Dance();
    }

    void Update()
    {
        Vector2 movementInput =playerInput.actions["Move"].ReadValue<Vector2>();
        float x = movementInput.x;
        float z = movementInput.y;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector3 move = transform.right * x + transform.forward * z;
        playerCharController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        playerCharController.Move(velocity * Time.deltaTime);

        
    }

    void Dance()
    {
        if (playerAnimation != null)
        {
            playerAnimation.SetTrigger("dance");
        }
    }
}
