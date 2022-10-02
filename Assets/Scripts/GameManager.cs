using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Game,
        Ending
    }
    [SerializeField] GameState currentState;
    [SerializeField] private ActivePlayer player01;
    [SerializeField] private ActivePlayer player02;
    
    //Canvas TurnCount
    [SerializeField] private GameObject canvasRound1;
    [SerializeField] private GameObject canvasRound2;
    [SerializeField] private GameObject canvasRound3;
    [SerializeField] private ActivePlayerHealth player01Health;
    [SerializeField] private ActivePlayerHealth player02Health;
    
    
    
    [SerializeField] private TurnManager turnManager;  
    [SerializeField] private LevelManager levelManager;  


    //Input var
    private PlayerInput playerInput;

    private void Awake()
    {
        player01.AssignManager(turnManager);
        player02.AssignManager(turnManager);
        playerInput = GetComponent<PlayerInput>();

        currentState = GameState.Game;
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case GameState.Game:
                Game();
                break;
            case GameState.Ending:
                EndingTime();
                break;
        }
        Debug.Log("Current turn is "+ turnManager.TurnNumber().ToString());
    }

    private void Game()
    {
        if (turnManager.TurnNumber() == 3)
        {
            canvasRound1.gameObject.SetActive(false);
            canvasRound2.gameObject.SetActive(true);
        }
        if (turnManager.TurnNumber() == 5)
        {
            canvasRound2.gameObject.SetActive(false);
            canvasRound3.gameObject.SetActive(true);
        }   
        if (turnManager.TurnNumber() == 7)
        {
            currentState = GameState.Ending;
        }
    }
    private void EndingTime()
    {
        levelManager.Player02Win();
    }    
    
    
    private void EndingDeadPlayer01()
    {
        if ( player01Health.currentHealth <= 0)
        {
            levelManager.Player02Win();

        }
    }
    private void EndingDeadPlayer02()
    {
        if ( player01Health.currentHealth <= 0)
        {
            levelManager.Player01Win();

        }
    }
}
