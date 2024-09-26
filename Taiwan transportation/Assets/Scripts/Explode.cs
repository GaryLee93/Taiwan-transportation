using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Explode : MonoBehaviour{
    enum ExType{Boss, Normal}
    [SerializeField] ExType type;
    VideoPlayer vp;
    SpriteRenderer sr;
    void Start(){
        vp = GetComponent<VideoPlayer>();
        vp.Prepare();
        
        sr = GetComponent<SpriteRenderer>();
        if(type == ExType.Boss){
            StartCoroutine(enlarge());
            StartCoroutine(longfadeOut());
        }
        else if(type == ExType.Normal){
            StartCoroutine(shortfadeOut());
        }
    }
    IEnumerator enlarge(){
        while(!vp.isPrepared){
            transform.localScale = Vector2.zero;
            yield return null;
        }
        vp.Play();
        float timer = 0, fadeTime = 2f;
        while(timer < fadeTime){
            timer += Time.deltaTime;
            if(timer >= fadeTime)
                timer = fadeTime;
            float rate = Mathf.Sqrt(timer / fadeTime);
            transform.localScale = new Vector3(0f+ 15* rate, 0f+ 15* rate, 1f);
            yield return null;
        }
    }

    IEnumerator longfadeOut(){
        yield return new WaitForSeconds(1f);
        float timer = 0f, fadeTime = 0.5f;
        while(timer < fadeTime){
            timer += Time.deltaTime;
            if(timer >= fadeTime)
                timer = fadeTime;
            float rate = 1- (timer / fadeTime);
            
            Color tmp = sr.color;
            tmp.a = rate;
            sr.color = tmp;

            yield return null;
        }
        Destroy(gameObject);
    }
    IEnumerator shortfadeOut(){
        Vector2 initScale = transform.localScale;
        while(!vp.isPrepared){
            transform.localScale = Vector2.zero;
            yield return null;
        }
        transform.localScale = initScale;
        vp.Play();
        float timer = 0f, fadeTime = 1f;
        while(timer < fadeTime){
            timer += Time.deltaTime;
            if(timer >= fadeTime)
                timer = fadeTime;
            float rate = 1- (timer / fadeTime);
            
            Color tmp = sr.color;
            tmp.a = rate;
            sr.color = tmp;

            yield return null;
        }
        Destroy(gameObject);
    }
    
}
