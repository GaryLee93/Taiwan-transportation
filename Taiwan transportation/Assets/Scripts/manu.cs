using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class manu : MonoBehaviour
{
    [System.Serializable] 
    public class Button
    {
        public string name;
        public GameObject button;
        public Sprite normalImg;
        public Sprite selectedImg;
    }
    [SerializeField] private List<Button> buttons;
    [SerializeField] private List<GameObject> buttonImgs;
    [SerializeField] AudioSource choseSound; 
    [SerializeField] AudioSource pressSound;
    [SerializeField] AudioSource menuMusic;
    [SerializeField] GameObject loadingAni;
    [SerializeField] GameObject backGround;
    [SerializeField] GameObject title;
    [SerializeField] GameObject weiShadow;
    [SerializeField] GameObject rain;
    Vector2 titlePos = new Vector2(0,2.7f);
    int nowSelected = 0;
    bool cnaChose = false;
    void Start() 
    {
        cnaChose = false;
        rain.GetComponent<VideoPlayer>().Prepare();
        menuMusic.Play();
        StartCoroutine(loadMenu());
        StartCoroutine(loadButton());
        loadingAni.GetComponent<VideoPlayer>().Prepare();
        buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg; 
    }
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && cnaChose) 
        {
            choseSound.Play();
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].normalImg;
            nowSelected = (nowSelected-1)%buttons.Count;
            if(nowSelected<0) nowSelected = (nowSelected+buttons.Count)%buttons.Count;
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && cnaChose) 
        {
            choseSound.Play();
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].normalImg;
            nowSelected = (nowSelected+1)%buttons.Count;
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        } 

        if(Input.GetKeyDown(KeyCode.Z))
        {
            pressSound.Play();
            if(nowSelected==0) StartGame();
            else if(nowSelected==2) QuitGame();
        }   

        if(rain.GetComponent<VideoPlayer>().isPrepared && !rain.GetComponent<VideoPlayer>().isPlaying) 
        {
            rain.GetComponent<SpriteRenderer>().enabled = true;
            rain.GetComponent<VideoPlayer>().Play();
        }
    }
    public void StartGame()
    {
        StartCoroutine(loadingStart());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
    IEnumerator loadingStart()
    {
        if(loadingAni.GetComponent<VideoPlayer>().isPrepared)
        {
            loadingAni.GetComponent<SpriteRenderer>().enabled = true;
            loadingAni.GetComponent<VideoPlayer>().Play();
        }
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Stage1");
    }
    IEnumerator loadMenu()
    {
        float timer=0f, fadeTimer=0.5f;
        title.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
        weiShadow.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);

        timer = 0f;
        fadeTimer = 0.5f;
        Vector2 diff = new Vector2(0,2.7f) - new Vector2(0,6f); 
        while(timer < fadeTimer){
            timer += Time.deltaTime;
            if(timer >= fadeTimer){
                timer = fadeTimer;
            }
            title.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
            title.transform.position = new Vector2(0,6f) + diff*Mathf.Sqrt((timer/fadeTimer));
            yield return null;
        }

        timer = 0f;
        fadeTimer = 0.5f;
        while(timer < fadeTimer){
            timer += Time.deltaTime;
            if(timer >= fadeTimer){
                timer = fadeTimer;
            }
            weiShadow.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
            yield return null;
        }
    }
    IEnumerator loadButton()
    {
        float timer=0f, fadeTimer=0.2f;
        Color color;
        for(int i=0;i<buttonImgs.Count;i++) 
        {
            color = buttonImgs[i].GetComponent<SpriteRenderer>().color;
            color.a = 0;
            buttonImgs[i].GetComponent<SpriteRenderer>().color = color;
        }
        for(int i=0;i<buttonImgs.Count;i++)
        {
            color = buttonImgs[i].GetComponent<SpriteRenderer>().color;
            while(timer < fadeTimer){
                timer += Time.deltaTime;
                if(timer >= fadeTimer){
                    timer = fadeTimer;
                }
                color.a = timer/fadeTimer;
                buttonImgs[i].GetComponent<SpriteRenderer>().color = color;
                yield return null;
            }
            timer = 0f;
        }
        cnaChose = true;
    }
}
