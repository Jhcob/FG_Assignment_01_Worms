using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    private TurnManager manager;
    
    public void AssignManager(TurnManager theManager)
    {
        manager = theManager;
    }

}
 