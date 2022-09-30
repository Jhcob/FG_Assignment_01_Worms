using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMine : MonoBehaviour
{
    //Bullet
    [SerializeField] private GameObject MinePrefab;    
    [SerializeField] private Transform MineSpawn;
    [SerializeField] private Transform MineParent;
    [SerializeField] private float mineDamage;




    public void DropMine()
    {
        Debug.Log("dropped mine");
        // RaycastHit hit;
        
        GameObject bullet = GameObject.Instantiate(MinePrefab, MineSpawn.position, Quaternion.identity, MineParent);
        // MineController mineController = bullet.GetComponent<MineController>();
        
        // ActivePlayerHealth activePlayerHealth = hit.collider.GetComponent<ActivePlayerHealth>();
        //
        // if (activePlayerHealth != null)
        // {
        //     activePlayerHealth.TakeDamage(mineDamage);
        // }

    }
    
    
    
    
    
    // public void FireProjectile()
    // {
    //     RaycastHit hit;
    //     GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletParent);
    //     BulletController bulletController = bullet.GetComponent<BulletController>();
    //     
    //     if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
    //     {
    //         bulletController.target = hit.point;
    //         bulletController.hit = true;
    //         ActivePlayerHealth activePlayerHealth = hit.collider.GetComponent<ActivePlayerHealth>();
    //         if (activePlayerHealth != null)
    //         {
    //             activePlayerHealth.TakeDamage(weaponDamage);
    //         }
    //     }
    //     else
    //     {
    //         bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
    //         bulletController.hit = false ;
    //     }  
    // }
}
