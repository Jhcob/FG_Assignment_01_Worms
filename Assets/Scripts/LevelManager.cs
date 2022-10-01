using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class LevelManager : MonoBehaviour
{
    // private InputAction AnyAction;
    // private PlayerInput playerInput;

    // Start is called before the first frame update
    // void Start()
    // {
    //     playerInput = GetComponent<PlayerInput>();
    //     AnyAction = playerInput.actions["Any"];

    
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            NextLevel();
        }
    }
    
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            Restart();
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
    
    void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
