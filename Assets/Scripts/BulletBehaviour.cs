using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] 
    private float decalLifeSpan = 2f; 
        
    // Start is called before the first frame update
    void OnEnable()
    {
        // Disabling decal after set time
        Invoke("OnDisable", decalLifeSpan);
    }

    private void OnDisable()
    {
        this.transform.gameObject.SetActive(false);
    }
}
