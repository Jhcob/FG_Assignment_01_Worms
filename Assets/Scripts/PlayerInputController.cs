using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerInputController : MonoBehaviour
{    
    private float movementSpeed; // Adjust at Move function
    [SerializeField] [Range(1f, 10f)] private float sprintSpeed = 10f;
    [SerializeField]  private float jumpHeight = 5f;
    [SerializeField] private float gravityValue = -10f;
    [SerializeField] [Range(0f, 1f)] float rotationSmoothTime = 0.1f;

    private Vector2 inputMove;
    //private Vector3 Velocity;

    // input button
    public bool inputJump = false;
    public bool inputSprint = false;

    // player
    private float targetRotation;
    private float rotationVelocity;
    private float verticalVelocity;

    
    public Transform mainCamera;
    //public Animator animator;

    private CharacterController _characterController;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
    }
    private void Update()
    {
        Move();
        Jump();
        Gravity();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputJump = context.ReadValueAsButton();
        }
        return;
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        inputSprint = context.ReadValueAsButton();
    }

    private void Move()
    {
        // normalise input direction
        Vector3 Velocity = new Vector3(inputMove.x, 0f, inputMove.y).normalized;
        
        if (Velocity.magnitude >= 0.1f)
        {
            movementSpeed = 6f;
        }
        else
        {
            movementSpeed = 0f;
        }

        if (inputSprint == true)
        {
            movementSpeed = sprintSpeed;
        }
     
        if (inputMove != Vector2.zero) // (Velocity.magnitude >= 0.1f && _characterController.isGrounded)
        {
            // rotation from 0 to input + camera angle
            targetRotation = Mathf.Atan2(Velocity.x, Velocity.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            // rotation smooth
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
                rotationSmoothTime);
            
            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0f, rotation, 0f);
        }
        //transform rotation to movement
        Vector3 targetDirection = Quaternion.Euler(0f, targetRotation, 0f) * (Vector3.forward);

        // convert vel to displacement and Move the character:
        _characterController.Move(targetDirection.normalized * (movementSpeed * Time.deltaTime) + new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }
    private void Jump()
    {
        if (_characterController.isGrounded)
        {
            if (inputJump == true)
            {
                //Debug.Log("jumping");
                // the square root of H * -2 * G = how much velocity needed to reach desired height
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
            }
        }
        else
        {
            Debug.Log("floating");
            // if we are not grounded, do not jump
            inputJump = false;
        }
    }
    private void Gravity()
    {
        if (_characterController.isGrounded)
        {
            //Debug.Log("grounded");
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
            

