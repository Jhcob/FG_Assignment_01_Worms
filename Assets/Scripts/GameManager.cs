using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class GameManager : MonoBehaviour
{
    enum GameState
    {
        Intro,
        Game,
        Ending
    }
    [SerializeField] GameState currentState;
    [SerializeField] public ActivePlayer player01;
    [SerializeField] public ActivePlayer player02;
    
    [SerializeField] private ActivePlayerManager manager;  


    //Input var
    private PlayerInput playerInput;

    private void Awake()
    {
        player01.AssignManager(manager);
        player02.AssignManager(manager);
        playerInput = GetComponent<PlayerInput>();

        
        currentState = GameState.Intro;
    }

    void LateUpdate()
    {
        switch (currentState)
        {
            case GameState.Game:
                Game();
                break;
            case GameState.Ending:
                Ending();
                break;
        }
    }

    private void Intro()
    {

    }
    private void Game()
    {
        
    }
    private void Ending()
    {
        
    }
}
