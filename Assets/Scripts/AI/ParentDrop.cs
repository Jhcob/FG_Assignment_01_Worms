using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentDrop : MonoBehaviour
{

    public GameObject roaster01;
    public GameObject roaster02;
    public GameObject roaster03;
    public bool isReady;
    public TurnManager _turnManager;
    public int turnNumber;

    // Start is called before the first frame update
    void Start()
    {
        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        turnNumber = _turnManager.TurnNumber();

        if ( turnNumber == 1 && isReady &&  Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log( "turn number is " + (_turnManager.TurnNumber().ToString()));

            roaster01.gameObject.transform.parent = null;
            isReady = false;
        }

        if (turnNumber == 3)
        {
            isReady = true;
            if (isReady &&  Input.GetKeyDown(KeyCode.E))
            {
                roaster02.gameObject.transform.parent = null;
                isReady = false;
            }
        }        
        
        if (turnNumber == 5)
        {
            isReady = true;
            if (isReady &&  Input.GetKeyDown(KeyCode.E))
            {
                roaster03.gameObject.transform.parent = null;
                isReady = false;
            }
        }
    }
}
