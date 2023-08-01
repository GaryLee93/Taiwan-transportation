using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class manu : MonoBehaviour
{
    [SerializeField] GameObject loadingAni;
    void Start() 
    {
        loadingAni.GetComponent<VideoPlayer>().Prepare(); 
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
        SceneManager.LoadScene("TestingStage");
    }
}
