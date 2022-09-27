using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePlayerShoot : MonoBehaviour
{
    [SerializeField] private bool inputShoot;
    [SerializeField] private ActivePlayerManager manager;
    private InputAction FireAction;
    private PlayerInput playerInput;



    // Start is called before the first frame update
    void Start()
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