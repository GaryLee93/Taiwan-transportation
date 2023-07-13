using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeling_shoot : MonoBehaviour
{
    public GameObject bullet;
    [SerializeField] float shoot_interval=0.05f;
    private float timer;
    void Start()
    {
        timer = 0;
    }
    void FixedUpdate()
    {   
        if(timer>=shoot_interval)
        {
            if(Input.GetKey(KeyCode.Z)) 
                shoot();
            timer-=shoot_interval;
        }
        timer+=Time.fixedDeltaTime;
    }
    public void shoot(){
        objectPooler.spawnFromPool(bullet.name,transform.position,
        transform.rotation,null);
    }
    
}

