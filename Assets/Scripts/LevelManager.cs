using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private int currentSceneIndex;

    void Update()
    {
        TitleScreen();
        GameOverScreen();
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

    void GameOverScreen()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; 

        if (currentSceneIndex == 2 || currentSceneIndex == 3)
        {
            if (Input.anyKey)
            {
                Restart();
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
