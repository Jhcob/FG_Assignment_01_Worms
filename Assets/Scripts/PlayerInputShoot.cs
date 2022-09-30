using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputShoot : MonoBehaviour
{
    [SerializeField] private ActivePlayerManager manager;   
    [SerializeField] private WeaponGun weaponGun;
    [SerializeField] private WeaponMine weaponMine;
    [SerializeField] public ActivePlayer player01;
    [SerializeField] public ActivePlayer player02;
    private InputAction FireAction;
    private PlayerInput playerInput;


    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        FireAction = playerInput.actions["Fire"];
    }

    private void Start()
    {
        player01.AssignManager(manager);
        player02.AssignManager(manager);
    }

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (FireAction.triggered)
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
}