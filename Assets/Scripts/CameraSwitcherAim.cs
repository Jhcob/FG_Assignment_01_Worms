using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class CameraSwitcherAim : MonoBehaviour
{
    [SerializeField]
    private TurnManager activePlayerManager;    
    [SerializeField]
    private PlayerInput inputManager;

    [SerializeField] 
    private Canvas Canvas;
    [SerializeField] 
    private Canvas CanvasAim;
    
    private int priorityHigh = 11;
    private int priorityLow = 9;
    
    [SerializeField]
    private CinemachineVirtualCamera aimCamera01;
    [SerializeField] 
    public ActivePlayer player01;
    [SerializeField] 
    public ActivePlayer player02;

    private ActivePlayer currentPlayer;
    private InputAction aimAction;

    private void Awake()
    {
        currentPlayer = activePlayerManager.GetCurrentPlayer();
        aimAction = inputManager.actions["Aim"];
    }

    private void Update()
    {
        AimCamera();
    }

    private void AimCamera()
    {currentPlayer = activePlayerManager.GetCurrentPlayer();  
        if (currentPlayer == player01)
        {    
            CanvasAim.enabled = false;
            Canvas.enabled = true;
            if (aimAction.IsPressed())
            {
                CanvasAim.enabled = true;
                Canvas.enabled = false;
                aimCamera01.Priority = priorityHigh;
            }
            else if (aimAction.WasReleasedThisFrame())
            {
                CanvasAim.enabled = false;
                Canvas.enabled = true;
                aimCamera01.Priority = priorityLow;
            }
        }
        if (currentPlayer == player02)
        {    
            CanvasAim.enabled = false;
            Canvas.enabled = false;
        }
    }
}
