using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class PauseMenu : absMenu
{
    public static bool gameIsPaused = false;
    public static PauseMenu instance;
    public delegate void PauseMenuEvent();
    public PauseMenuEvent pause,resume;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject loadingAni;
    [SerializeField] AudioSource pauseSound;
    Player player;
    void Awake() 
    {
        instance = this;
        player = Player.instance;
        pause = new PauseMenuEvent(pauseTime); 
        resume = new PauseMenuEvent(resumeTime);
        loadingAni.GetComponent<VideoPlayer>().Prepare();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && player.playerData.remain_life>=0)
        {
            if(pauseMenu==null)
            {
                Debug.Log("miss");
                return;
            }
            if(gameIsPaused) resume();
            else pause();
        }

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
            if(nowSelected==0) resume();
            else if(nowSelected==1) StartFromStage1();
            else if(nowSelected==2)  exitGame();
        }
    }
    public void gameContinue()
    {
        pauseMenu.SetActive(false);
        player.gameContinue();
        gameIsPaused = false;
        resume();
    } 
    public void resumeTime()
    {
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        canChose = false;
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
        nowSelected = 0;
        buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameIsPaused = true;
        canChose = true;
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
