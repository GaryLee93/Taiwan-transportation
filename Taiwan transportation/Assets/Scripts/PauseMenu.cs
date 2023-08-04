using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public delegate void PauseMenuEvent();
    public PauseMenuEvent pause,resume;
    [SerializeField] GameObject pauseMenu;
    void Awake() 
    {
        pause = new PauseMenuEvent(pauseTime); 
        resume = new PauseMenuEvent(resumeTime);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused) resume();
            else pause();
        }
    }

    public void resumeTime()
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
    public void pauseTime()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameIsPaused = true;
        Debug.Log("pause");
    }
}
