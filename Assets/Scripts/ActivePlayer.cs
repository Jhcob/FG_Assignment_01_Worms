using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    private ActivePlayerManager manager;
    public void AssignManager(ActivePlayerManager theManager)
    {
        manager = theManager;
    }

    public void FireProjectile()
    {
        SetRandomColor();
        manager.ChangeTurn();
    }
    
    public void SetRandomColor()
    {
        GetComponent<MeshRenderer>().material.color =
            new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f),1f); 
    }
}
 