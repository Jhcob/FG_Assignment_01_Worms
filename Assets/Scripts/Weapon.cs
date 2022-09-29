using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Bullet
    [SerializeField] private GameObject bulletPrefab;    
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletMissDistance = 25f;
    
    public Transform cameraTransform;

    public void FireProjectile()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletParent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletController.hit = false ;
        }  
    }
}
