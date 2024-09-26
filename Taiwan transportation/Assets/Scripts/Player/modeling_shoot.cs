using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeling_shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] float shoot_interval;
    [SerializeField] AudioSource shootSound;
    Player player;
    private float timer;
    void Start()
    {
        player = Player.instance;
        timer = 0;
    }
    void FixedUpdate()
    {   
        if(player.canShoot && Input.GetKey(KeyCode.Z)){
            if(player.canShoot && timer <=0){
                shoot();
                timer = shoot_interval;
            }
            timer -= Time.fixedDeltaTime;
        }
        else
            timer = 0;
    }
    public void shoot(){
        if(bullet != null)
            objectPooler.spawnFromPool(bullet.name,transform.position,new Quaternion());
        if(shootSound != null)
            shootSound.Play();
    }
    
}

