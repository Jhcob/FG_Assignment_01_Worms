using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraInputController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera Player01;
    [SerializeField] private CinemachineVirtualCamera Player02;
    
    private PlayerInput playerInput;
   
    private InputAction swithTurnAction;


    private void OnEnable()
    {
        CameraSwitcher.Register(Player01);
        CameraSwitcher.Register(Player02);
        CameraSwitcher.SwitchCamera(Player01);
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        swithTurnAction = playerInput.actions["SwitchTurn"];
    }

    private void Update()
    {
        SwitchTurn();
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(Player01);
        CameraSwitcher.Unregister(Player02);
    }

    public void SwitchTurn()
    {
        if (swithTurnAction.triggered)
        {
            Debug.Log("Camera switch");
            if (CameraSwitcher.IsActiveCamera(Player01))
            {
                CameraSwitcher.SwitchCamera(Player02);
            }
            else if (CameraSwitcher.IsActiveCamera(Player02))
            {
                CameraSwitcher.SwitchCamera(Player01);
            }
        }
    }

}
