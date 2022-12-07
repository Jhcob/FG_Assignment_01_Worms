using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputShoot : MonoBehaviour
{
    [SerializeField] private TurnManager manager;   
    [SerializeField] private WeaponGun weaponGun;
    [SerializeField] private WeaponMine weaponMine;
    [SerializeField] public ActivePlayer player01;
    [SerializeField] public ActivePlayer player02;
    private InputAction fireAction, specialAction;
    private PlayerInput playerInput;
    private ActivePlayer currentPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        currentPlayer = manager.GetCurrentPlayer();

        playerInput = GetComponent<PlayerInput>();
        fireAction = playerInput.actions["Fire"];
        specialAction = playerInput.actions["Special"];
    }

    private void Start()
    {
        player01.AssignManager(manager);
        player02.AssignManager(manager);
    }

    private void Update()
    {
        Shoot();
        ShootSpecial();
    }

    public void Shoot()
    {
        if (fireAction.triggered)
        {
            ActivePlayer currentPlayer = manager.GetCurrentPlayer();
            if (currentPlayer == player01)
            {
                weaponGun.FireProjectile();

            }
            if (currentPlayer == player02)
            {
                weaponMine.DropMine();

            }
        }
    }

    public void ShootSpecial()
    {
        ActivePlayer currentPlayer = manager.GetCurrentPlayer();

        if (specialAction.triggered && currentPlayer == player02)
        {
            weaponMine.RoasterSpawn();
            Debug.Log("special!");

        }
    }
}