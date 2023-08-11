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
    [SerializeField] GameObject loadingAni;
    [SerializeField] GameObject backGround;
    [SerializeField] GameObject title;
    [SerializeField] GameObject weiShadow;
    Vector2 titlePos = new Vector2(0,2.7f);
    int nowSelected = 0;
    void Start() 
    {
        StartCoroutine(loadMenu());
        StartCoroutine(loadButton());
        loadingAni.GetComponent<VideoPlayer>().Prepare();
        buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg; 
    }
    void Update() 
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && nowSelected>0) 
        {
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].normalImg;
            nowSelected = (nowSelected-1)%buttons.Count;
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        }
        if(Input.GetKeyDown(KeyCode.DownArrow) && nowSelected<(buttons.Count-1)) 
        {
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].normalImg;
            nowSelected = (nowSelected+1)%buttons.Count;
            buttons[nowSelected].button.GetComponent<SpriteRenderer>().sprite = buttons[nowSelected].selectedImg;
        } 

        if(Input.GetKeyDown(KeyCode.Z))
        {
            if(nowSelected==0) StartGame();
            else if(nowSelected==2) QuitGame();
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
        backGround.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
        title.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
        weiShadow.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
        while(timer < fadeTimer){
            timer += Time.deltaTime;
            if(timer >= fadeTimer){
                timer = fadeTimer;
            }
            backGround.GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
            yield return null;
        }

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
        for(int i=0;i<buttonImgs.Count;i++) buttonImgs[i].GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
        for(int i=0;i<buttonImgs.Count;i++)
        {
            while(timer < fadeTimer){
                timer += Time.deltaTime;
                if(timer >= fadeTimer){
                    timer = fadeTimer;
                }
                buttonImgs[i].GetComponent<SpriteRenderer>().color = new Color(timer/fadeTimer, timer/fadeTimer, timer/fadeTimer, timer/fadeTimer);
                yield return null;
            }
            timer = 0f;
        }
    }
}
