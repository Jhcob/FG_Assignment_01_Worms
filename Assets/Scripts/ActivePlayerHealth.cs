using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth;
    [SerializeField] private Image healthBar;

    public float currentHealth; 
    
    void Start()
    {
        currentHealth = maxHealth;

        healthBar.fillAmount = 1f;
    }

    private void Update()
    {
        Debug.Log(currentHealth.ToString());
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.fillAmount = currentHealth / maxHealth;

        if (currentHealth <= 0)
        {
            //Die
            Debug.Log("you are dead");
            
            // Destroy(gameObject);
        }
    }


} 
