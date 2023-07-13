using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spellCardPerform : MonoBehaviour
{
    GameObject title;
    AudioSource sound;
    Vector2 iniPos,Finalpos,iniSize,FinalSize,smallDire,MoveDire;
    bool[] textMoveCheck = new bool[2];//0 for smallize,1 for move
    float[] textTime = new float[2];//0 for smallize,1 for move
    float smallizeSpeed,MoveSpeed;
    public static spellCardPerform instance;
    void Awake() 
    {
        instance = this;
        sound = GetComponent<AudioSource>();
        sound.Play();
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
                if(i==0) title.transform.localScale += (Vector3)smallDire*smallizeSpeed*Time.fixedDeltaTime;
                else transform.Translate(MoveSpeed*MoveDire*Time.fixedDeltaTime);
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
        title = transform.GetChild(0).gameObject;
        title.GetComponent<Text>().text = CardName;
        iniPos = new Vector2(2f,-6f);
        Finalpos = new Vector2(2f,6f);
        iniSize = new Vector2(20f,20f);
        FinalSize = new Vector2(5f,5f);
        transform.position = iniPos;
        title.transform.localScale = iniSize;
        #endregion

        for(int i=0;i<2;i++) textMoveCheck[i]=false;
        textMoveCheck[0] = true;
        smallDire = FinalSize - (Vector2)title.transform.localScale;
        MoveDire = Finalpos - (Vector2)title.transform.position;
        smallizeSpeed = smallDire.magnitude/time;
        MoveSpeed = MoveDire.magnitude/moveTime;
        MoveDire.Normalize();
        smallDire.Normalize();
        textTime[0] = time;
        textTime[1] = moveTime;
        title.SetActive(true);
    }
    public void retriveTitle()
    {
        title.SetActive(false);
        transform.position = iniPos;
        title.transform.localScale = iniSize;
    }
}
