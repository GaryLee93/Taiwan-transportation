using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Informer : MonoBehaviour
{
    public enum Infromation{bonusGet, bounsFailed, powerUp,powerFull}
    public static Informer instance;
    static Queue<Infromation> waitToInform;
    float timer;
    bool outputCheck,stayCheck,retriveCheck;
    void Awake() 
    {
        waitToInform = new Queue<Infromation>();
        instance = this;    
    }
    void FixedUpdate() 
    {
        if(timer>0) timer -= Time.fixedDeltaTime;
        inform();
    }
    void inform()
    {
        if(waitToInform.Count>0)
        {
            transform.position = new Vector2(-6,4);
            if(waitToInform.Peek() == Infromation.bonusGet) GetComponent<Text>().text = "Spell Card Bonus gotten!!!";
            else if (waitToInform.Peek() == Infromation.bounsFailed) GetComponent<Text>().text = "Bonus failed...";
            else if(waitToInform.Peek() == Infromation.powerUp) GetComponent<Text>().text = "Power Up";
            else if(waitToInform.Peek() == Infromation.powerFull) GetComponent<Text>().text = "Full Power Mode";
            else GetComponent<Text>().text = "WTF";
            outputCheck = true;
            timer = 0.5f;
            waitToInform.Dequeue();
        }

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
        if(stayCheck)
        {
            if(timer<=0)
            {
                stayCheck = false;
                retriveCheck = true;
                timer = 0.5f;
            }
        }
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
    public static void titleOutput(Infromation type)
    {
        waitToInform.Enqueue(type);
    }
}
