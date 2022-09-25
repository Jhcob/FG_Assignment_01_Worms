using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class PlayerInputController : MonoBehaviour
{
    private CharacterController _characterController;  
    private Vector2 _moveValue;  
    [SerializeField] [Range(0, 1f)] private float movementSpeed = 1;

  
    private void Awake()  
    {  
        _characterController = GetComponent<CharacterController>();  
    }  
  
    public void Move(InputAction.CallbackContext context)  
    {  
        _moveValue = context.ReadValue<Vector2>();  
    }  
    

  
    private void FixedUpdate()  
    {  
        var moveVector = new Vector3(_moveValue.x, 0, _moveValue.y); // To move in 3d  
        _characterController.Move(moveVector * movementSpeed);  
    }
}
