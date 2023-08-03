using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    [SerializeField] float fadeTime;
    SpriteRenderer spr;
    float timer;
    Color tmpcolor;
    void Start(){
        timer = 0f;
        spr = GetComponent<SpriteRenderer>();
        tmpcolor = new Color(1,1,1,0);
        spr.color = tmpcolor;
    }
    void Update(){
        if(Input.GetKey(KeyCode.LeftShift)){
            tmpcolor.a = 1f * (timer/fadeTime);
            spr.color = tmpcolor;
            timer += Time.deltaTime;
            if(timer >= fadeTime)
                timer = fadeTime;
        }
        else{
            tmpcolor.a = 1f * (timer/fadeTime);
            spr.color = tmpcolor;
            timer -= Time.deltaTime;
            if(timer <= 0f)
                timer = 0f;
        }
    }
}
