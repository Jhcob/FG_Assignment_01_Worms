using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] 
    private CinemachineVirtualCamera Player01;
    [SerializeField] 
    private CinemachineVirtualCamera Player02;
    
    [SerializeField]
    private ActivePlayerManager turnManager;

    private void ChangeTurnInput(ActivePlayerManager myManager)
    {
        turnManager = myManager;
    }

    private void OnEnable()
    {
        CameraSwitcher.Register(Player01);
        CameraSwitcher.Register(Player02);
        CameraSwitcher.SwitchCamera(Player01);
    }

    public void CameraSwitch()
    {
        if (CameraSwitcher.IsActiveCamera(Player01))
        {
            CameraSwitcher.SwitchCamera(Player02);
        }
        else if (CameraSwitcher.IsActiveCamera(Player02))
        {
            CameraSwitcher.SwitchCamera(Player01);
        }
    }
    private void OnDisable()
    {
        CameraSwitcher.Unregister(Player01);
        CameraSwitcher.Unregister(Player02);
    }
}
