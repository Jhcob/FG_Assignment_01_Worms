using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerInputController : MonoBehaviour
{    
 [SerializeField] [Range(1f, 10f)] private float movementSpeed = 5f;
 [SerializeField] [Range(1f, 10f)] private float sprintSpeed = 10f;

    [SerializeField] [Range(15f, 50f)] private float jumpHeight = 15f;
    [SerializeField][Range(0.0f, 0.3f)] public float RotationSmoothTime = 0.12f;

    [SerializeField] private float gravityValue = -10f;

    private Vector2 inputMove;
    private Vector3 moveDir;
    private Vector3 Velocity;
    [SerializeField] [Range(0f, 1f)] float rotationSmoothTime = 0.1f;
    private float rotationVelocity;


    private float targetRotation;
    private float verticalVelocity;
    private float _terminalVelocity = 53.0f;

    // timeout deltatime
    private float _jumpTimeoutDelta;
    private float _fallTimeoutDelta;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;
    
    private int isMoving;
    public bool inputSprint = false;
    public bool Grounded = true;
    public float GroundedOffset = -0.14f;
    public float GroundedRadius = 0.28f;
    public LayerMask GroundLayers;
    
    public float JumpTimeout = 0.50f;
    
    public Transform cam;

    private CharacterController _characterController;
    //public Animator animator;


    private void Awake()
    {
        //animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        // Cursor.visible = false;
        
    }
    
    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
            transform.position.z);
        Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
            QueryTriggerInteraction.Ignore);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<Vector2>();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        inputSprint = context.ReadValueAsButton();
    }

    // public void Jump()
    // {
    //     //moveVector.y = jumpHeight;
    // }

    // public void OnJump(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         Debug.Log("Jump pressed");
    //         Jump();
    //     }
    //
    //     return;
    // }

    private void Update()
    {
        Move();
        JumpAndGravity();
     }

    private void Move()
    {
      
        // normalise input direction
        Velocity = new Vector3(inputMove.x, 0f, inputMove.y).normalized;
        
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
            targetRotation = Mathf.Atan2(Velocity.x, Velocity.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
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
      
        Debug.Log("On the ground");
    }

    private void JumpAndGravity()
    {
        if (Grounded)
        {
            // stop our velocity dropping infinitely when grounded
            if (verticalVelocity < 0.0f)
            {
                verticalVelocity = -2f;
            }
            
            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (verticalVelocity < _terminalVelocity)
            {
                verticalVelocity += gravityValue;
            }
        }
        else
        {
            // reset the jump timeout timer
            // _jumpTimeoutDelta = JumpTimeout;

            // fall timeout
            // if (_fallTimeoutDelta >= 0.0f)
            // {
            //     _fallTimeoutDelta -= Time.deltaTime;
            // }
            // else
            // {
            //     // // update animator if using character
            //     // if (_hasAnimator)
            //     // {
            //     //     _animator.SetBool(_animIDFreeFall, true);
            //     // }
            // }

            // // if we are not grounded, do not jump
            // _input.jump = false;
        }
        
        // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
        if (verticalVelocity < _terminalVelocity)
        {
            verticalVelocity += gravityValue * Time.deltaTime;
        }

    }
}
            

