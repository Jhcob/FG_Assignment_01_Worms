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

    private void Awake()
    {
        player01.AssignManager(turnManager);
        player02.AssignManager(turnManager);

        currentState = GameState.Game;
    }

    private void Update()
    {
        EndingDeadPlayer01();
        EndingDeadPlayer02();
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case GameState.Game:
                Game();
                break;
            case GameState.Ending:
                GameOver_player02Win();
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
            //player02victory
            levelManager.Player02Win();
        }
    }

    
    private void EndingDeadPlayer01()
    {
        if ( player01Health.health <= 0)
        {
            GameOver_player02Win();

        }
    }
    private void EndingDeadPlayer02()
    {
        if ( player02Health.health <= 0)
        {
            GameOver_player01Win();
        }
    }

    public void GameOver_player01Win()
    {
        levelManager.Player01Win();
    }
    
    private void GameOver_player02Win()
    {
        levelManager.Player02Win();
    }    

}
