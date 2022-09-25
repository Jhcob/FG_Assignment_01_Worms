using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraInputController : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook Player01;
    [SerializeField] private CinemachineFreeLook Player02;

    private void OnEnable()
    {
        CameraSwitcher.Register(Player01);
        CameraSwitcher.Register(Player02);
        CameraSwitcher.SwitchCamera(Player01);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(Player01);
        CameraSwitcher.Unregister(Player02);
    }


    public void OnTurnOver(InputAction.CallbackContext context)
    {
        if (context.performed)
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
