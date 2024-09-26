using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour{
    CameraShake instance;
    static bool isShaking;
    static float timer;
    static float c_duration;
    static float c_amount;
    static float c_interval;
    static Vector3 init_pos;
    private void Start(){
        instance = this;
        isShaking = false;
        init_pos = transform.position;
    }
    private void Update(){
        if(isShaking){
            if(timer >= c_interval){
                float dx = Random.Range(-c_amount, c_amount);
                float dy = Random.Range(-c_amount, c_amount);
                transform.position = init_pos + new Vector3(dx, dy, 0);
                timer -= c_interval;
                c_duration -= c_interval;
            }
            if(timer >= c_duration){
                isShaking = false;
                transform.position = init_pos;
            }
            timer += Time.deltaTime;
        }
    }

    public static void shakeCamera(float duration, float amount, float interval){
        if(isShaking){
            Debug.LogWarning("is shaking");
        }
        else{
            timer = 0;
            c_duration = duration;
            c_amount = amount;
            c_interval = interval;
            isShaking = true;
        }
    }
}
