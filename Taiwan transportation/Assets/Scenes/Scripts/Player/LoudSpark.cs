using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoudSpark : MonoBehaviour{
    [SerializeField] float fadeTime;
    [SerializeField] float holdTime;
    [SerializeField] float shootInterval;
    [SerializeField] GameObject LSparkCircle;
    [SerializeField] AudioSource sparkSound;
    float main_timer;
    float shoot_timer;
    void Start(){
        transform.localScale = new Vector3(0, 1, 0);
        main_timer = 0f;
        CameraShake.shakeCamera(fadeTime + holdTime, 0.05f, 0.1f);
        sparkSound.Play();
    }
    void Update(){
        if(main_timer <= fadeTime){
            transform.localScale = new Vector3(main_timer / fadeTime, 1, 0);
        }
        else if(main_timer > fadeTime && main_timer <= fadeTime + holdTime){
            transform.localScale = new Vector3(1, 1, 0);
        }
        else if(main_timer > fadeTime +holdTime && main_timer <= fadeTime*2 +holdTime){
            transform.localScale = new Vector3(1- (main_timer -fadeTime -holdTime) /fadeTime, 1, 0);
        }
        else{
            Destroy(gameObject);
        }
        main_timer += Time.deltaTime;
    }
    void FixedUpdate(){
        if(main_timer <= fadeTime +holdTime){
            if(shoot_timer >= shootInterval){
                GameObject tmp = Instantiate(LSparkCircle, transform);
                tmp.transform.localPosition = new Vector3(0, -6f, 0);
                shoot_timer -= shootInterval;
            }
        }
        shoot_timer += Time.fixedDeltaTime;    
    }
}
