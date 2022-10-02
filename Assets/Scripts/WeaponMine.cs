using System;
using UnityEngine;
using UnityEngine.UI;

public class WeaponMine : MonoBehaviour
{
    [SerializeField] private TurnManager turnManager;    
    [SerializeField] private GameObject MinePrefab;    
    [SerializeField] private Transform MineSpawn;
    private float mineDelayBase;
    [SerializeField] public float mineDelay = 2f;
    [SerializeField] private int minesMax;
    [SerializeField] private int mines;
    [SerializeField] private Image[] minesSprites;

    [SerializeField] private Sprite fullMine, emptyMine;

    private void Update()
    {
        ReloadMines();
        PlayerMines();
        if (mineDelayBase > 0)
        {
            mineDelayBase -= Time.deltaTime;
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

    public void ReloadMines()
    {
        if (turnManager.TurnNumber()%2 == 0)
        {
            mines = minesMax;
        }
    }

    public void DropMine()
    {
        if (mines > 0)
        {
            
            if (mineDelayBase <= 0f)
            {
                mines--;
                GameObject bullet = GameObject.Instantiate(MinePrefab, MineSpawn.position, Quaternion.identity);
                Debug.Log("dropped mine");
                mineDelayBase = mineDelay;
            }
        }
    }
    
}
