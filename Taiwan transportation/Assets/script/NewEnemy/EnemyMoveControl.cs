using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveControl : MonoBehaviour
{
    public class fumoType{
        public int type;
        public Vector2 direction;
        public float time;
        public fumoType(int type, Vector2 direction, float time){
            this.type = type;
            this.direction = direction;
            this.time = time;
        }
    }
    private Rigidbody2D rb;
    private Vector2 dest,slowMoveAccel;
    private float timer;
    private bool slowMoveCheck=false,MoveCheck=false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timer=0;
        slowMoveCheck=false;
        MoveCheck=false;
    }
    void FixedUpdate() 
    {
        if(slowMoveCheck)
        {
            rb.velocity += slowMoveAccel*Time.fixedDeltaTime;
            timer -= Time.fixedDeltaTime;
            if(timer<=0) 
            {
                rb.velocity = new Vector2(0,0);
                slowMoveCheck = false;
                MoveCheck = false;
            }
        }
    }
    public void slowDownMove(Vector2 displace,float time)
    {
        if(slowMoveCheck)
        {
            Debug.Log("can't set");
            return ;
        }
        MoveCheck = true;
        slowMoveCheck = true;
        timer = time;
        slowMoveAccel = countAccel(displace,time);
        rb.velocity = iniVelcity(displace,time);
    }
    public bool isMove()
    {
        return MoveCheck;
    }
    Vector2 countAccel(Vector2 d,float t)
    {
        return d*(-2f)/(t*t);
    }
    Vector2 iniVelcity(Vector2 d,float t)
    {
        Vector2 a = countAccel(d,t);
        float x,y;
        x = -a.x*t;
        y = -a.y*t;
        return new Vector2(x,y);
    }
}