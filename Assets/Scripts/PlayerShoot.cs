using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] 
    private ActivePlayerManager manager;
    private InputAction FireAction;
    private PlayerInput playerInput;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        FireAction = playerInput.actions["Fire"];
    }


    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (FireAction.triggered)
        {
            Debug.Log("Fire");
            ActivePlayer currentPlayer = manager.GetCurrentPlayer();
            currentPlayer.FireProjectile();
        }
    }
    
    
}