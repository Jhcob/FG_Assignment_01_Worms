using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMine : MonoBehaviour
{
    //Bullet
    [SerializeField] private GameObject MinePrefab;    
    [SerializeField] private Transform MineSpawn;
    // [SerializeField] private Transform MineParent;
    [SerializeField] private float mineDamage;
    private float mineDelayBase;
    [SerializeField] public float mineDelay = 2f;


    private void Update()
    {
        if (mineDelayBase > 0)
        {
            mineDelayBase -= Time.deltaTime;
        }
    }

    public void DropMine()
    {
        if (mineDelayBase <= 0f)
        {
            GameObject bullet = GameObject.Instantiate(MinePrefab, MineSpawn.position, Quaternion.identity);
            Debug.Log("dropped mine");
            mineDelayBase = mineDelay;
        }

    }
    
    
}
