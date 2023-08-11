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
    [SerializeField] GameObject loadingAni;
    int nowSelected = 0;
    void Start() 
    {
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
}
