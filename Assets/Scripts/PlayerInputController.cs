using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerInputController : MonoBehaviour
{    
    [SerializeField] private ActivePlayerManager manager;

    private float speed = 7f; // Starting speed
    [SerializeField] [Range(3f, 10f)]private float movementSpeed = 6f; 
    [SerializeField] [Range(7f, 20f)] private float sprintSpeed = 12f;
    [SerializeField]  private float jumpHeight = 5f;
    [SerializeField] private float gravityValue = -10f;
    [SerializeField] [Range(0f, 10f)] float rotationSpeed = 5f;

    private Vector2 inputMove;

    // player
    private float rotationVelocity;
    private float verticalVelocity;
    
    public Transform cameraTransform;

    private CharacterController characterController;
    private PlayerInput playerInput;
    private ActivePlayerShoot _playerShoot;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction sprintAction;
    //public Animator animator;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        sprintAction = playerInput.actions["Sprint"];

        cameraTransform = Camera.main.transform;

        //animator = GetComponent<Animator>();

        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    private void Update()
    {
        Move();
        Jump();
        Gravity();
    }
    
    private void Move()
    {
        ActivePlayer currentPlayer = manager.GetCurrentPlayer();

        if (manager.PlayerCanPlay())
        {
            // Getting input Vector2 for moving
            Vector2 inputMove = moveAction.ReadValue<Vector2>();
            
            // normalise input direction
            Vector3 velocity = new Vector3(inputMove.x, 0f, inputMove.y).normalized;
            
            // Sprint 
            if (sprintAction.IsPressed() && velocity.magnitude >= 0.1f  && currentPlayer.GetComponent<CharacterController>().isGrounded)
            {
                speed = sprintSpeed;
            }
            else
            {
                speed = movementSpeed;
            }

            velocity = velocity.x * cameraTransform.right.normalized + velocity.z * cameraTransform.forward.normalized;
            velocity.y = 0;
            
            // rotate towards camera direction
            Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
            currentPlayer.GetComponent<CharacterController>().transform.rotation = Quaternion.Lerp( currentPlayer.GetComponent<CharacterController>().transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            currentPlayer.GetComponent<CharacterController>().Move((velocity * Time.deltaTime * speed) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
        }   
    }
    private void Jump()
    {
        ActivePlayer currentPlayer = manager.GetCurrentPlayer();

        if (currentPlayer.GetComponent<CharacterController>().isGrounded )
        {
            if (jumpAction.triggered)
            {
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            }
        }
    }
    private void Gravity()
    {
        ActivePlayer currentPlayer = manager.GetCurrentPlayer();

        if (currentPlayer.GetComponent<CharacterController>().isGrounded)
        {
            // stop our velocity dropping infinitely when grounded
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }
        }
        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        verticalVelocity += gravityValue * Time.deltaTime;
    }
}
            

