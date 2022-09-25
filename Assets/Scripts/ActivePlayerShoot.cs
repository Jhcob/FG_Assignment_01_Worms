using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivePlayerShoot : MonoBehaviour
{
    [SerializeField] private bool inputShoot;
    [SerializeField] private ActivePlayerManager manager;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //inputShoot = context.ReadValueAsButton();
            ActivePlayer currentPlayer = manager.GetCurrentPlayer();
            currentPlayer.FireProjectile();

            Debug.Log("Bang?");
        }
    }

    // public void Shoot()
    // {
    //         if (inputShoot == true)
    //         {
    //             Debug.Log("Shoot");
    //
    //         }
    //
    // }
}