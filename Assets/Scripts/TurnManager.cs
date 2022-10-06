using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class TurnManager : MonoBehaviour
{
    enum TurnState
    {
        PlayerTurn,
        WaitingForInput,
        TurnChange,
    }
    
    [Header("TurnSetting")]
    [SerializeField] private float maxTimePerTurn;
    [SerializeField] private float TurnDelayAfterDamage = 3f;
    public int turnCount;
    private float currentTurnTime;
    
    [Header("Check distance between players")]
    [SerializeField] private LayerMask PlayerMine;
    [SerializeField] private float radiusDeath = 2f;
    [SerializeField] private Transform player01Position;
    [SerializeField] private int meleeDamage =1;
        
    [Header("DangerZone")]
    [SerializeField] private DangerZone dangerZone;
    [SerializeField] private float DangerZoneDelay = 2f;

    [Header("Managers")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private PlayerInput playerInput;
    
    [Header("References")]
    [SerializeField] TurnState currentState;
    [SerializeField] public ActivePlayer player01;
    [SerializeField] public ActivePlayer player02;
    [SerializeField] private ActivePlayerHealth player01Health;
    [SerializeField] private ActivePlayerHealth player02Health;
    private ActivePlayer currentPlayer;
    
    [Header("UI")]
    [SerializeField] private Image timeBar;
    [SerializeField] GameObject buttonToPlay;
    [SerializeField] GameObject hudPlayer01;
    [SerializeField] GameObject hudPlayer02;    
    [SerializeField] GameObject hudAmmoPlayer02;
    [SerializeField] GameObject tooClose;
    [SerializeField] GameObject meleeAttack;

    private bool neverDone;
    private bool neverDoneMeleePlayer01;
    private bool neverDoneMeleePlayer02;


    private void Start()
    {
        turnCount = 1;
        neverDone = true;
        neverDoneMeleePlayer01 = true;
        neverDoneMeleePlayer02 = true;
        player01.AssignManager(this);
        player02.AssignManager(this);

        currentPlayer = player02;
        
        currentState = TurnState.PlayerTurn;
        buttonToPlay.gameObject.SetActive(false);
    }

    private void Update()
    {
        Player02MineDamaged();
        DamageDistance();
        GetCurrentPlayer();
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case TurnState.PlayerTurn:
                StartGame();
                break;
            case TurnState.WaitingForInput:
                InputToPlay();
                break;
            case TurnState.TurnChange:
                TurnOver();
                break;
        }
    }

    private void StartGame()
    {
        currentTurnTime += Time.deltaTime;
        if (currentTurnTime >= maxTimePerTurn)
        {
            TurnStart();
        }
        UpdateTimeVisuals();
    }

    private void TurnStart()
    {
        hudPlayer01.gameObject.SetActive(false);
        hudPlayer02.gameObject.SetActive(false);           
        tooClose.gameObject.SetActive(false);
        meleeAttack.gameObject.SetActive(false);

        ChangeCamera();
        PlayerCannotPlay();
        currentState = TurnState.WaitingForInput;
    }

    private void InputToPlay()
    {
        playerInput.enabled = false;

        buttonToPlay.gameObject.SetActive(true);
        currentTurnTime = 0f;
        if (Input.anyKey)
        {

            GameStateTurnChange();
        }
    }
    private void TurnOver()
    {
        Invoke("DangerZoneGrow", DangerZoneDelay );

        turnCount++;
        PlayerCanPlay();
        ChangeTurn();
        ResetTimers();
        currentState = TurnState.PlayerTurn;
        playerInput.enabled = true;

    }

    public int TurnNumber()
    {
        return turnCount;
    }

    public void GameStateTurnChange()
    {
        currentState = TurnState.TurnChange;
    }

    private void PlayerCanPlay()
    {
        currentPlayer.GetComponent<CharacterController>().enabled = true;
    }    
    private void PlayerCannotPlay()
    {
        currentPlayer.GetComponent<CharacterController>().enabled = false;
    }

    public ActivePlayer GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public void ChangeTurn()
    {
        buttonToPlay.gameObject.SetActive(false);

        if (player01 == currentPlayer)
        {
            hudPlayer01.gameObject.SetActive(false);
            hudPlayer02.gameObject.SetActive(true);
            hudAmmoPlayer02.gameObject.SetActive(true);
            
            currentPlayer = player02;
        }
        else if (player02 == currentPlayer)
        {
            hudPlayer01.gameObject.SetActive(true);
            hudPlayer02.gameObject.SetActive(false);
            hudAmmoPlayer02.gameObject.SetActive(false);
            currentPlayer = player01;
        }

        ResetTimers();
        UpdateTimeVisuals();
    }

    public void ChangeCamera()
    {
        cameraController.CameraSwitch();
    }
    
    private void ResetTimers()
    {
        currentTurnTime = 0;
    }

    private void UpdateTimeVisuals()
    {
        timeBar.fillAmount = 1 - (currentTurnTime / maxTimePerTurn);
       // seconds.text = Mathf.RoundToInt(maxTimePerTurn - currentTurnTime).ToString();
    }

    private void Player02MineDamaged()
    {
        if (neverDone)
        {
            if (player02Health.CurrentLife() == 1)
            {
                
                Invoke("DamageChangeTurn", TurnDelayAfterDamage);
                neverDone = false;
            }
        }
    }

    public void DamageChangeTurn()
    {
        turnCount++;
        TurnStart();
    }

    private void DamageDistance()
    {
        if (Physics.CheckSphere(player01Position.position, radiusDeath, PlayerMine))
        {
            if (neverDoneMeleePlayer01 && player01 == currentPlayer)
            {
                player02Health.GetComponent<ActivePlayerHealth>().TakeDamage(meleeDamage);
                meleeAttack.gameObject.SetActive(true);
                ResetTimers();
                neverDoneMeleePlayer01 = false;
                neverDoneMeleePlayer02 = false;
            }
            
            if (neverDoneMeleePlayer02 && player02 == currentPlayer)
            {
                tooClose.gameObject.SetActive(true);
                ResetTimers();
                Invoke("TooCloseToPlayer01", 3f);
            }
        }
    }
    private void DangerZoneGrow()
    {
        dangerZone.StartGrow();
    }

    private void TooCloseToPlayer01()
    {
        gameManager.GameOver_player01Win();
    }
}
