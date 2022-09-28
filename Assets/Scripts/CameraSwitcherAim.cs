using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;


public class CameraSwitcherAim : MonoBehaviour
{
    [SerializeField]
    private ActivePlayerManager activePlayerManager;
    
    private int priorityHigh = 11;
    private int priorityLow = 9;
    
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera01;
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera02;

    
    private ActivePlayer currentPlayer;
    
    [SerializeField] public ActivePlayer player01;
    [SerializeField] public ActivePlayer player02;

    private void Update()
    {
        currentPlayer = activePlayerManager.GetCurrentPlayer();
        AimCamera();
    }

    private void AimCamera()
    {
        if (currentPlayer == player01)
        {
            if (Mouse.current.rightButton.isPressed)
            {
                virtualCamera01.Priority = priorityHigh;
            }
            else if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                virtualCamera01.Priority = priorityLow;
            }
        }
        if (currentPlayer == player02)
        {
            if (Mouse.current.rightButton.isPressed)
            {
                virtualCamera02.Priority = priorityHigh;
            }
            else if (Mouse.current.rightButton.wasReleasedThisFrame)
            {
                virtualCamera02.Priority = priorityLow;

            }
        }
    }
}
