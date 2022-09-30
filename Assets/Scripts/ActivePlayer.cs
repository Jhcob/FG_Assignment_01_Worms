using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    private ActivePlayerManager manager;
    // [SerializeField]
    // private WeaponGun weaponGun;  
    // [SerializeField]
    // private WeaponMine weaponMine;
    
    
    public void AssignManager(ActivePlayerManager theManager)
    {
        manager = theManager;
    }
    //
    // public void FireProjectile()
    // {
    //     weaponGun.FireProjectile();
    //     SetRandomColor();
    // }
    //
    // public void DropMine()
    // {
    //     weaponMine.DropMine();
    //     SetRandomColor();
    // }
    //
    // public void SetRandomColor()
    // {
    //     GetComponent<MeshRenderer>().material.color =
    //         new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f),1f); 
    // }
}
 