using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private Transform dangerTransform;
    private Vector3 scaleZone;
    private float growFactor;
    [SerializeField] private float defaultGrowFactor;
    
    private float stopGrow = 0;
    [SerializeField] private int DangerZoneDamage = 1;
    
    
    private ParticleSystem myParticleSystem;
    [SerializeField] private TurnManager turnManager;


    private void Start()
    {
        growFactor = defaultGrowFactor;
    }

    void Update()
    {
        GrowDanger();
    }

    private void GrowDanger()
    {
        float timer = 0;
        timer += Time.deltaTime;
        dangerTransform.localScale += new Vector3(1, 0, 1) * Time.deltaTime * growFactor;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        ActivePlayerHealth activePlayerHealth = other.GetComponent<ActivePlayerHealth>();
        if (activePlayerHealth != null && ((other.gameObject.tag == "Player01") || (other.gameObject.tag == "Player02")))
        {
            Debug.Log("hit player");
            activePlayerHealth.TakeDamage(DangerZoneDamage);
            //gameObject.GetComponent<MeshRenderer>().enabled = false;
            //explosionFX.Play();
            turnManager.DamageChangeTurn();
            growFactor = stopGrow;

        }
    }

    public void StartGrow()
    {
        growFactor = defaultGrowFactor;
    }
}
