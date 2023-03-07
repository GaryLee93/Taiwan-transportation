using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour
{
    objectPooler instance;
    [SerializeField]GameObject bullet;
    [SerializeField]Rigidbody2D rb;
    [SerializeField]float movespeed = 1f;
    [SerializeField]float shootTime = 1f;
    float _timer = 0f;
    void Start(){
        instance=objectPooler.instance;
        rb.velocity = Vector2.left * movespeed;
    }
    void Update(){

        if(_timer > shootTime){
            behavior1();
            _timer -= shootTime;
        }
        _timer += Time.deltaTime;
        
        
        if(hitBorder())
            gameObject.SetActive(false);
    }

    void behavior1(){
        instance.spawnFromPool(bullet.name,transform.GetChild(0).transform.position,
            transform.GetChild(0).transform.rotation);
    }

    private bool hitBorder(){
        return this.transform.position.x <-9 || this.transform.position.x > 3.5 || 
                this.transform.position.y > 7 || this.transform.position.y < -7 ;
    }
}
