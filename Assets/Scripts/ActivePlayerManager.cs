using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActivePlayerManager : MonoBehaviour
{
    enum GameState
    {
        PlayerTurn,
        WaitingForInput,
        TurnChange,
    }

    [SerializeField] GameState currentState;
    [SerializeField] public ActivePlayer player01;
    [SerializeField] public ActivePlayer player02;
    [SerializeField] private float maxTimePerTurn;
    
    [Header("UI")]
    [SerializeField] private Image clock;
    [SerializeField] private TextMeshProUGUI seconds;
    [SerializeField] GameObject buttonToPlay;
    GameObject buttonToPlayEnable;

    [SerializeField] 
    private CameraController cameraController;
    
    private ActivePlayer currentPlayer;
    private float currentTurnTime;
    // private float currentDelay;
    

    private void Start()
    {
        player01.AssignManager(this);
        player02.AssignManager(this);

        currentPlayer = player01;
        
        currentState = GameState.PlayerTurn;
        buttonToPlay.gameObject.SetActive(false);
        buttonToPlayEnable = buttonToPlay.gameObject;
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case GameState.PlayerTurn:
                StartGame();
                break;
            case GameState.WaitingForInput:
                InputToPlay();
                break;
            case GameState.TurnChange:
                TurnOver();
                break;
        }
    }

    private void StartGame()
    {
        currentTurnTime += Time.deltaTime;
        if (currentTurnTime >= maxTimePerTurn)
        {
            ChangeCamera();
            PlayerCannotPlay();
            currentState = GameState.WaitingForInput;
        }
        UpdateTimeVisuals();
    }
    private void InputToPlay()
    {
        buttonToPlayEnable.SetActive(true);
        currentTurnTime = 0f;
        if (Input.GetKeyDown(KeyCode.T))
        {
            GameStateTurnChange();
        }
    }
    private void TurnOver()
    {
        PlayerCanPlay();
        ChangeTurn();
        ResetTimers();
        currentState = GameState.PlayerTurn;
    }

    public void GameStateTurnChange()
    {
        currentState = GameState.TurnChange;
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
        buttonToPlayEnable.SetActive(false);

        if (player01 == currentPlayer)
        {
            currentPlayer = player02;
        }
        else if (player02 == currentPlayer)
        {
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
        // currentDelay = timeBetweenTurns;
    }

    private void UpdateTimeVisuals()
    {
        clock.fillAmount = 1 - (currentTurnTime / maxTimePerTurn);
        seconds.text = Mathf.RoundToInt(maxTimePerTurn - currentTurnTime).ToString();
    }
}
