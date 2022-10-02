using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    private int currentSceneIndex;
    // private InputAction AnyAction;
    // private PlayerInput playerInput;

    // Start is called before the first frame update
    // void Start()
    // {
    //     playerInput = GetComponent<PlayerInput>();
    //     AnyAction = playerInput.actions["Any"];

    
    // Update is called once per frame

    private void Awake()
    {

    }

    void Update()
    {
        TitleScreen();
Debug.Log(currentSceneIndex.ToString());
    }

    void TitleScreen()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 

        if (currentSceneIndex == 0)
        {
            if (Input.anyKey)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    
    public void NextLevel()
    {
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(3);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public void Player01Win()
    {
        SceneManager.LoadScene("GameOverScreen_Spider");
    }    
    public void Player02Win()
    {
        SceneManager.LoadScene("GameOverScreen_Chick");
    }
    
}
