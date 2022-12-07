using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMine : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;    
    [SerializeField] private GameObject minePrefab, aiEnemy, parentObject;    
    [SerializeField] private Transform mineSpawn;
    private float mineDelayBase;
    [SerializeField] public float mineDelay = 2f;
    [SerializeField] private int minesMax;
    [SerializeField] private int roastersMax;
    [SerializeField] private int mines;
    [SerializeField] private int roasters;
    [SerializeField] private Image[] minesSprites;
    [SerializeField] private Image[] roasterSprites;

    [SerializeField] private Sprite fullMine, emptyMine;
    [SerializeField] private Sprite fullRoaster, emptyRoaster;

     
    [Header("RoasterSpawner")]
    public GameObject roaster01, roaster02, roaster03;
    public bool isReady;
    public TurnManager _turnManager;
    public int turnNumber;


    private void Start()
    {
        isReady = true;

    }


    private void Update()
    {
        turnNumber = _turnManager.TurnNumber();

        
        ReloadAmmo();
        PlayerMinesUI();
        PlayerRoasterUI();
        if (mineDelayBase > 0)
        {
            mineDelayBase -= Time.deltaTime;
        }
    }

    public void RoasterSpawn()
    {
        if ( turnNumber == 1 && isReady)
        {
            roasters--;
            Debug.Log("dropped roaster");
            roaster01.SetActive(true);
            roaster01.gameObject.transform.parent = null;
            isReady = false;
        }

        if (turnNumber == 3)
        {
            isReady = true;
            if (isReady)
            {
                roasters--;
                Debug.Log("dropped roaster");
                roaster02.SetActive(true);

                roaster02.gameObject.transform.parent = null;
                isReady = false;
            }
        }        
        
        if (turnNumber == 5)
        {
            isReady = true;
            if (isReady)
            {
                roasters--;
                Debug.Log("dropped roaster");
                roaster03.SetActive(true);

                roaster03.gameObject.transform.parent = null;
                isReady = false;
            }
        }    
    }

    private void PlayerRoasterUI()
    {
        // If implement health pickups
        if (roasters > roastersMax)
        {
            roasters = roastersMax;
        }
        
        for (int i = 0; i < roasterSprites.Length; i++)
        {
            if (i < roasters)
            {
                roasterSprites[i].sprite = fullRoaster;
            }
            else
            {
                roasterSprites[i].sprite = emptyRoaster;
            }

            if (i < roastersMax)
            {
                roasterSprites[i].enabled = true;
            }
            else
            {
                roasterSprites[i].enabled = false;
            }
        }

    }

    private void PlayerMinesUI()
    {
        // If implement health pickups
        if (mines > minesMax)
        {
            mines = minesMax;
        }
        
        for (int i = 0; i < minesSprites.Length; i++)
        {
            if (i < mines)
            {
                minesSprites[i].sprite = fullMine;
            }
            else
            {
                minesSprites[i].sprite = emptyMine;
            }

            if (i < minesMax)
            {
                minesSprites[i].enabled = true;
            }
            else
            {
                minesSprites[i].enabled = false;
            }
        }
    }

    public void ReloadAmmo()
    {
        if (turnManager.TurnNumber()%2 == 0)
        {
            mines = minesMax;
            roasters = roastersMax;
        }
    }


    public void DropMine()
    {
        if (mines > 0)
        {
            if (mineDelayBase <= 0f)
            {
                mines--;
                GameObject bullet = GameObject.Instantiate(minePrefab, mineSpawn.position, Quaternion.identity);
                Debug.Log("dropped mine");
                mineDelayBase = mineDelay;
            }
        }
    }
    
}
