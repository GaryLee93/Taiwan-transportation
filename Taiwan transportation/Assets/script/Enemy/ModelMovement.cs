using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelMovement : MonoBehaviour
{
    public class fumoType
    {
        public int type;
        public Vector2 direction;
        public float time;
        public fumoType(int type, Vector2 direction, float time)
        {
            this.type = type;
            this.direction = direction;
            this.time = time;
        }
    }
    private List<fumoType> moveList;
    private List<fumoType> accelList;
    private Rigidbody2D rb;
    private Clock clock;
    private int move_index = 0;
    private int accel_index = 0;
    private bool moveButton = false,accelButton = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        clock = Clock.clockInstance;
    }
    public void setMovement(int type,Vector2 dire,float time)
    {
        if(moveList==null) moveList = new List<fumoType>();
        fumoType tem = new fumoType(type,dire,time);
        moveList.Add(tem);
    }
    public void setAccel(int type,Vector2 dire,float time)
    {
        if(moveList==null) accelList = new List<fumoType>();
        fumoType tem = new fumoType(type,dire,time);
        accelList.Add(tem);
    }
    public void startMove()
    {
        moveButton = true;
    }
    public void startAccel()
    {
        moveButton = true;
    }
    private void Update() 
    {
        if(moveButton && move_index < moveList.Count)
        {
            int type = moveList[move_index].type;
            Vector2 direction = moveList[move_index].direction;
            float time = moveList[move_index].time;
    
            if(clock.checkTimer("moveTimer"))  return;
            else if(type == 0)
            {
                // do nothing
            }
            else if(type == 1)
            {
                rb.velocity = direction;
            }
            else if(type == 2)
            {
                Debug.Log("3sec");
            }
            else if(type == 3)
            {
                Debug.Log("5sec");  
            }
 
            if(type == -1) move_index = 0;
            else move_index++;
            clock.setTimer("moveTimer",time);

            if(move_index==moveList.Count) moveButton=false;
        }

        if(accelButton && accel_index < accelList.Count)
        {
            int type = accelList[accel_index].type;
            Vector2 direction = accelList[accel_index].direction;
            float time = accelList[accel_index].time;

            if(clock.checkTimer("accelTimer")) return;
            else if(type == 0)
            {
                // do nothing
            }
            else if(type == 1)
            {
                while(time > 0)
                {
                    rb.velocity += direction*Time.deltaTime;
                }
            }
            else if(type == 2)
            {
                Debug.Log("3sec");
            }
            else if(type == 3)
            {
                Debug.Log("5sec");
            }

            if(type == -1) accel_index = 0;
            else accel_index++;
            clock.setTimer("accelTimer",time);

            if(accel_index == accelList.Count) accelButton = false;
        }
    }
  
}