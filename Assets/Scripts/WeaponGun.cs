using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGun : MonoBehaviour
{
    //Bullet
    [SerializeField] private GameObject bulletPrefab;    
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private Transform bulletParent;
    [SerializeField] private float bulletMissDistance = 25f;
    [SerializeField] private float weaponDamage;
    private float gunDelayBase;
    [SerializeField] private float gunDelay = 0.5f;
    public Transform cameraTransform;

    private void Update()
    {
        if (gunDelayBase > 0)
        {
            gunDelayBase -= Time.deltaTime;
        }
    }


    public void FireProjectile()
    {
        if (gunDelayBase <= 0f)
        {
            RaycastHit hit;
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity, bulletParent);
            BulletController bulletController = bullet.GetComponent<BulletController>();
        
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, Mathf.Infinity))
            {
                bulletController.target = hit.point;
                bulletController.hit = true;
                ActivePlayerHealth activePlayerHealth = hit.collider.GetComponent<ActivePlayerHealth>();
                if (activePlayerHealth != null)
                {
                    activePlayerHealth.TakeDamage(weaponDamage);
                }
            }
            else
            {
                bulletController.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
                bulletController.hit = false ;
            }  
            gunDelayBase = gunDelay;

        }
      
    }
}
