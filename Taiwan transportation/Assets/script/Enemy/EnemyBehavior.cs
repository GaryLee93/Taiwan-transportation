using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    objectPooler instance;
    [SerializeField]GameObject bullet;
    [SerializeField]Rigidbody2D rb;
    public int moveType = 0;
    public int shootType = 0;
    public float movespeed = 2f;
    public Vector2 moveVector = new Vector2(0f, 0f);
    public float shootFreq = 1f;
    float _timer = 0f;
    void Start(){
        instance=objectPooler.instance;
    }
    void Update(){
        if(moveType == 1)
            move1();
        else if(moveType == 2)
            move2();
        
        /*if(shootType == 1)
            shoot1();*/
        


        if(hitBorder())
            Destroy(gameObject);
    }

    void move1(){
        rb.velocity = moveVector;
    }

    void move2(){
        rb.velocity = -moveVector;
    }
    void shoot1(){
        instance.spawnFromPool(bullet.name,transform.GetChild(0).transform.position,
            transform.GetChild(0).transform.rotation);
    }

    

    bool hitBorder(){
        return this.transform.position.x <-9 || this.transform.position.x > 3.5 || 
                this.transform.position.y > 7 || this.transform.position.y < -7 ;
    }
}
