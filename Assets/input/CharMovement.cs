using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharMovement : MonoBehaviour
{

    [Header("Components of the character")]
    [SerializeField] private Rigidbody rigB;
    [SerializeField] private Transform camera;
    [SerializeField] private PlayerInput playerInput;

    [Header("Movement values")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime;
    private Vector3 dir;

    [Header("Animator")]
    [SerializeField] Animator anim;
    int isWalkingHash;
    int isRunningHash;

    private float _turnSmoothVelocity;

    private void Start()
    {
        anim = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
    }

    private void Update()
    {
        Vector2 movementInput = playerInput.actions["Move"].ReadValue<Vector2>();
        float horizontal = movementInput.x;
        float vertical = movementInput.y;
        dir = new Vector3(horizontal, 0f, vertical).normalized;


        if (dir.magnitude != 0f)
        {
            anim.SetBool("isRunning", true);
            CharacterMove();
        }
        else anim.SetBool("isRunning", false);
    }

    private void CharacterMove()
    {
        //mantiene al jugador orientado en la direccion del movimiento
        float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + camera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);


        //Usamos el char.controller para movernos con la direccion que sacamos del imput system
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        rigB.AddForce(moveDir.normalized * speed * Time.deltaTime, ForceMode.Force);
    }

    void HandleMovement()
    {
        bool isWalking = anim.GetBool("isWalkingHash");
        bool isRunning = anim.GetBool("isRunningHash");
    }
}
