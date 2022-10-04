using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMine : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;    
    [SerializeField] private GameObject minePrefab, roasterPrefab;    
    [SerializeField] private Transform mineSpawn, roasterSpawn;
    private float mineDelayBase;
    [SerializeField] public float mineDelay = 2f;
    [SerializeField] private int minesMax;
    [SerializeField] private int mines;
    [SerializeField] private Image[] minesSprites;
    [SerializeField] private Sprite roasterSprite;

    [SerializeField] private Sprite fullMine, emptyMine;
    [SerializeField] private Sprite fullRoaster, emptyRoaster;
    public bool isRoasterUsed;

     
     
    private void Update()
    {
        ReloadAmmo();
        PlayerMines();
        if (mineDelayBase > 0)
        {
            mineDelayBase -= Time.deltaTime;
        }
    }

    private void PlayerRoaster()
    {
        if (isRoasterUsed = false)
        {
            roasterSprite = emptyRoaster;
        }
        else
        {
            roasterSprite = fullRoaster;
        }
    }

    private void PlayerMines()
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
            isRoasterUsed = false;
        }
    }

    public void DropRoaster()
    {
        if (isRoasterUsed == false)
        {
            GameObject bullet = GameObject.Instantiate(roasterPrefab, roasterSpawn.position, Quaternion.identity);
            Debug.Log("dropped roaster");
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
