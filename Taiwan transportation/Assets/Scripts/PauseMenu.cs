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
    [SerializeField] GameObject ContinueMenu;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject loadingAni;
    [SerializeField] AudioSource pauseSound;
    GameObject temMenu;
    Player player;
    void Awake() 
    {
        player = Player.instance; 
        temMenu = pauseMenu;
        pause = new PauseMenuEvent(pauseTime); 
        resume = new PauseMenuEvent(resumeTime);
        player.gameOver = new Player.playerEvent(callContinueMenu);
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
    void callContinueMenu()
    {
        temMenu = ContinueMenu;
        gameIsPaused = true;
        pause();
    }
    public void gameContinue()
    {
        temMenu.SetActive(false);
        player.gameContinue();
        gameIsPaused = false;
        resume();
        temMenu = pauseMenu;
    } 
    public void resumeTime()
    {
        gameIsPaused = false;
        temMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Resume()
    {
        resume();
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
        pauseSound.Play();
        Time.timeScale = 0f;
        temMenu.SetActive(true);
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
