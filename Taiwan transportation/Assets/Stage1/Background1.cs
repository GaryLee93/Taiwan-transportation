using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Background1 : MonoBehaviour
{
    [SerializeField] GameObject taxifield;
    [SerializeField] GameObject changefield;
    [SerializeField] GameObject walkfield;
    [SerializeField] GameObject walkchange;
    [SerializeField] GameObject bangfield;
    [SerializeField] GameObject blackfield;
    [SerializeField] GameObject titlePic;

    VideoPlayer taxiVp;
    VideoPlayer changeVp;
    VideoPlayer walkVp;
    VideoPlayer walkchangeVp;
    VideoPlayer bangVp;

    public VideoPlayer nowPlaying;

    void Start(){
        taxiVp = taxifield.GetComponent<VideoPlayer>();
        walkVp = walkfield.GetComponent<VideoPlayer>();
        changeVp = changefield.GetComponent<VideoPlayer>();
        bangVp= bangfield.GetComponent<VideoPlayer>();
        walkchangeVp = walkchange.GetComponent<VideoPlayer>();

        nowPlaying = null;

        taxiVp.Prepare();
        walkVp.Prepare();
        changeVp.Prepare();
        walkchangeVp.Prepare();
        bangVp.Prepare();
    }

    void Update(){
        if(nowPlaying != null){
            if(Input.GetKey(KeyCode.Pause)){
                
            }
        }
        
    }
    
    public void start_taxi(){
        StartCoroutine(taxi());
    }
    IEnumerator taxi(){
        while(!taxiVp.isPrepared){
            yield return null;
        }
        taxiVp.Play();
        nowPlaying = taxiVp;

        float a_rate = 1f;
        while(a_rate>=0){
            if(a_rate >= 1)
                a_rate = 1;
            blackfield.GetComponent<SpriteRenderer>().color = new Color(0.1f,0.1f,0.1f,a_rate);
            a_rate -= Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5f);
    }
    public void display_title(){
        StartCoroutine(title());
    }
    IEnumerator title(){
        yield return new WaitForSeconds(2f);
        float a_rate=0;
        while(a_rate<1){
            a_rate += Time.deltaTime;
            if(a_rate >= 1)
                a_rate = 1;
            titlePic.GetComponent<SpriteRenderer>().color = new Color(1,1,1,a_rate);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        while(a_rate>0){
            a_rate -= Time.deltaTime;
            if(a_rate <= 0)
                a_rate = 0;
            titlePic.GetComponent<SpriteRenderer>().color = new Color(1,1,1,a_rate);
            yield return null;
        }
    }

    public void start_change(){
        changeVp.Play();
        nowPlaying = changeVp;
        taxiVp.Stop();
        StartCoroutine(change());
    }
    IEnumerator change(){
        while(changeVp.isPlaying){
            yield return null;
        }
        walkVp.Play();
        nowPlaying = walkVp;
    }

    public void start_walkchange(){
        StartCoroutine(walk_change());
    }
    IEnumerator walk_change(){
        walkVp.loopPointReached += vpAfterloop;
        while(walkVp.isPlaying){
            yield return null;
        }
        walkchangeVp.Play();
        nowPlaying = walkchangeVp;
    }
    void vpAfterloop(VideoPlayer vp){
        vp.Stop();
    }
}
