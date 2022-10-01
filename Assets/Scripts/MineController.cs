using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineController : MonoBehaviour
{
    // [SerializeField] private float speed = 1f;
    [SerializeField] private float mineDamage;
    private ParticleSystem myParticleSystem;
    [SerializeField] private ParticleSystem explosionFX;
    private float timeToDestroy = 0.6f;



    private void OnEnable()
    {
        explosionFX.Stop();

    }

    private void OnTriggerEnter(Collider other)
    {
        ActivePlayerHealth activePlayerHealth = other.GetComponent<ActivePlayerHealth>();
        if (activePlayerHealth != null && other.gameObject.tag == "Player01")
        {
            activePlayerHealth.TakeDamage(mineDamage);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            explosionFX.Play();
            Destroy(gameObject, timeToDestroy);
        }
    }

    private void OnCollisionEnter(Collision other)
    {

    }
}
