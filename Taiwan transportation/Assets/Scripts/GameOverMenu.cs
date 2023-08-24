using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class GameOverMenu : absMenu
{
    public static GameOverMenu instance;
    public delegate void GameOverEvent();
    public GameOverEvent Continue;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject loadingAni;
    [SerializeField] AudioSource pauseSound;
    Player player;
    void Awake() 
    {
        instance = this;
        player = Player.instance;
        player.gameOver += pauseTime;
        Continue = new GameOverEvent(resumeTime); 
    }
    void Update() 
    {
        chose();    
    }
    protected override void chose()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && canChose) 
        {
            choseSound.Play();
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].normalImg;
            nowSelected = (nowSelected-1)%buttons.Count;
            if(nowSelected<0) nowSelected = (nowSelected+buttons.Count)%buttons.Count;
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && canChose) 
        {
            choseSound.Play();
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].normalImg;
            nowSelected = (nowSelected+1)%buttons.Count;
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        }

        if(Input.GetKeyDown(KeyCode.Z) && canChose)
        {
            pressSound.Play();
            if(nowSelected==0) Continue();
            else if(nowSelected==1) StartFromStage1();
            else if(nowSelected==2)  exitGame();
        }
    }
    public void exitGame()
    {
        PauseMenu.gameIsPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }
    void resumeTime()
    {
        player.playerData.remain_life = 3;
        player.playerData.score = 0;
        player.refreshLifeText();
        player.refreshScoreText();
        PauseMenu.gameIsPaused = false;
        gameOverMenu.SetActive(false);
        Time.timeScale = 1f;
        canChose = false;
    }
    void pauseTime()
    {
        pauseSound.Play();
        nowSelected = 0;
        buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        Time.timeScale = 0f;
        gameOverMenu.SetActive(true);
        PauseMenu.gameIsPaused = true;
        canChose = true;
    }
    public void StartFromStage1()
    {
        gameOverMenu.SetActive(false);
        StartCoroutine(loadingStart());
        PauseMenu.gameIsPaused = false;
        Time.timeScale = 1f;
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
