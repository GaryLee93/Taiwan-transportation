using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class bossPicPerform : MonoBehaviour
{
    GameObject pic;
    Vector2 dire,iniPos;
    AudioSource spellSound;
    bool[] picMoveCheck = new bool[3];//0 for move,1 for delay,2 for depart
    float[] picTime = new float[3];//0 for move,1 for felay,2 for depart
    float speed;
    public static bossPicPerform instance;
    void Awake() 
    {
        instance = this;
    }
    void FixedUpdate() 
    {
        moveToCenter();    
    }
    void moveToCenter()
    {
        for(int i=0;i<3;i++)
        {
            if(picMoveCheck[i])
            {
                picTime[i] -= Time.fixedDeltaTime;
                if(i==0 || i==2) transform.Translate(speed*dire*Time.fixedDeltaTime);
                
                if(picTime[i]<=0f)
                {
                    picMoveCheck[i]=false;
                    if(i==2) transform.position = iniPos;
                    else picMoveCheck[i+1]=true;
                }
                break;
            }
        }
    }
    public void startMove(float second,float delay)
    {
        spellSound = GetComponent<AudioSource>();
        for(int i=0;i<3;i++) picMoveCheck[i]=false;
        iniPos = transform.position;
        pic = transform.GetChild(1).gameObject;
        picMoveCheck[0]=true;
        dire = new Vector2(-1,-1) - (Vector2)transform.position;
        picTime[0] = second;
        picTime[1] = delay;
        picTime[2] = 1f;
        speed = dire.magnitude/picTime[0];
        dire.Normalize();
        pic.SetActive(true);
        spellSound.Play(0);
    }
}
