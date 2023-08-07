using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BonusInform : MonoBehaviour
{
    public static BonusInform instance;
    float timer;
    bool outputCheck,stayCheck,retriveCheck;
    void Awake() 
    {
        instance = this;    
    }
    void FixedUpdate() 
    {
        if(timer>0) timer -= Time.fixedDeltaTime;
        output();
        stay();
        retrive();
    }
    void output()
    {
        if(outputCheck)
        {
            transform.Translate((new Vector2(0,4)-new Vector2(-6,4))/0.5f*Time.fixedDeltaTime);
            if(timer<=0)
            {
                timer=1f;
                outputCheck = false;
                stayCheck = true;
            }
        }
    }
    void stay()
    {
        if(stayCheck)
        {
            if(timer<=0)
            {
                stayCheck = false;
                retriveCheck = true;
                timer = 0.5f;
            }
        }
    }
    void retrive()
    {
        if(retriveCheck)
        {
            transform.Translate((new Vector2(15,4)-new Vector2(0,4))/0.5f*Time.fixedDeltaTime);
            if(timer<=0)
            {
                timer=0f;
                retriveCheck = false;
            }
        }
    }
    public void titleOutput(bool won)
    {
        transform.position = new Vector2(-6,4);
        if(won) GetComponent<Text>().text = "Bonus gotten!!!";
        else GetComponent<Text>().text = "Bonus failed...";
        outputCheck = true;
        timer = 0.5f;
    }
}
