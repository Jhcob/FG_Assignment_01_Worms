using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletController : MonoBehaviour
{
    [SerializeField] private GameObject[] decalBullet;
    [SerializeField] private float speed = 50f;
    
    private float timeToDestroy = 3f;
    
    public Vector3 target { get; set; }
    public bool hit { get; set; }

    private void OnEnable()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        BulletMovement();
    }

    private void BulletMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < .01f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        ContactPoint hitInfo = other.GetContact(0);
        // Random range of decals
        GameObject decalsRandom = decalBullet[Random.Range(0, decalBullet.Length)];
        
        // Instantiate bullet decals
        GameObject.Instantiate(decalsRandom, hitInfo.point + hitInfo.normal * 0.0001f, Quaternion.LookRotation(hitInfo.normal));
        Destroy(gameObject);
    }
}
