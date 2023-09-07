using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellCardPerform : MonoBehaviour
{
    GameObject title;
    AudioSource sound;
    Vector2 iniPos,Finalpos,iniSize = new Vector2(20f,20f),FinalSize = new Vector2(1f,1f),smallDire,MoveDire;
    bool[] textMoveCheck = new bool[2];//0 for smallize,1 for move
    float[] textTime = new float[2];//0 for smallize,1 for move
    float[] desireTime = new float[2];
    float MoveSpeed;
    public static spellCardPerform instance;
    void Awake() 
    {
        instance = this;
        title = transform.GetChild(0).gameObject;
    }    
    void FixedUpdate()
    {
        titleSmallize();
    }
    void titleSmallize()
    {
        for(int i=0;i<2;i++)
        {
            if(textMoveCheck[i])
            {
                textTime[i] -= Time.fixedDeltaTime;
                if(i==0) 
                {
                    title.transform.localScale = (Vector3)(iniSize - (iniSize - FinalSize)*((desireTime[0]-textTime[0])/desireTime[0]));
                    if(title.transform.localScale.magnitude < FinalSize.magnitude) title.transform.localScale = FinalSize;
                }
                else title.transform.Translate(MoveSpeed*MoveDire*Time.fixedDeltaTime);
                if(textTime[i]<=0f)
                {
                    textMoveCheck[i]=false;
                    if(i!=1) textMoveCheck[i+1]=true; 
                }
                break;
            }
        }      
    }
    public void startSmallize(string CardName,float time,float moveTime)
    {
        #region setIniValue
        title.GetComponent<Text>().text = CardName;
        iniPos = new Vector2(0.85f,-6f);
        Finalpos = new Vector2(0.85f,6f);
        title.transform.position = iniPos;
        title.transform.localScale = iniSize;
        #endregion

        for(int i=0;i<2;i++) textMoveCheck[i]=false;
        textMoveCheck[0] = true;
        smallDire = FinalSize - (Vector2)title.transform.localScale;
        MoveDire = Finalpos - (Vector2)title.transform.position;
        MoveSpeed = MoveDire.magnitude/moveTime;
        MoveDire.Normalize();
        smallDire.Normalize();
        textTime[0] = time;
        desireTime[0] = time;
        textTime[1] = moveTime;
        desireTime[1] = moveTime;
        title.SetActive(true);
    }
    public void retriveTitle()
    {
        title.SetActive(false);
        title.transform.position = iniPos;
        title.transform.localScale = iniSize;
    }
}
