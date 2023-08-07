using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public delegate void PauseMenuEvent();
    public PauseMenuEvent pause,resume;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject loadingAni;
    void Awake() 
    {
        pause = new PauseMenuEvent(pauseTime); 
        resume = new PauseMenuEvent(resumeGame);
        loadingAni.GetComponent<VideoPlayer>().Prepare();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused) resume();
            else pause();
        }
    }

    public void resumeGame()
    {
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void StartFromStage1()
    {
        pauseMenu.SetActive(false);
        StartCoroutine(loadingStart());
        gameIsPaused = false;
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
    }
    IEnumerator loadingStart()
    {
        if(loadingAni.GetComponent<VideoPlayer>().isPrepared)
        {
            loadingAni.GetComponent<SpriteRenderer>().enabled = true;
            loadingAni.GetComponent<VideoPlayer>().Play();
        }
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Stage1");
    }
}
