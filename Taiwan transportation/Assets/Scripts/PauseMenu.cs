using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] GameObject pauseMenu;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Pause))
        {
            if(gameIsPaused) resume();
            else pause();
        }
    }

    public void resume()
    {
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void exitGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
    void pause()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameIsPaused = true;
    }
}
