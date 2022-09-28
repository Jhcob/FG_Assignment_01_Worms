using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class CameraSwitcherAim_Player02 : MonoBehaviour
{
    [SerializeField]
    private ActivePlayerManager activePlayerManager;
    
    [SerializeField]
    //private PlayerInput playerInput;
    private InputAction aimAction;

    // [SerializeField] 
    // private int priorityBoosAmount = 10;
    
    private CinemachineVirtualCamera virtualCamera;

    private ActivePlayer currentPlayer;
    
    [SerializeField] public ActivePlayer player02;
    
   
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        // playerInput = GetComponent<PlayerInput>();
        // aimAction = playerInput.actions["Aim"];    
    }

    private void Start()
    {
    }

    private void Update()
    {
        currentPlayer = activePlayerManager.GetCurrentPlayer();
        AimCamera();
        

    }

    private void AimCamera()
    {
        if (Mouse.current.rightButton.isPressed && currentPlayer == player02)
        {
            virtualCamera.Priority = 11;
            Debug.Log("Right mouse button pressed pl02");
        }        
        
        if (Mouse.current.rightButton.wasReleasedThisFrame && currentPlayer == player02)
        {
            virtualCamera.Priority = 9;
            Debug.Log("Right mouse button released pl02");
        }
    }
}
