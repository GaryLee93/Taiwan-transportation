using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Clock : MonoBehaviour
{
    private Dictionary<string,float> timers;
    private Vector2 up,down;
    private float spellCardTimer;
    private bool spellCardCount,upCheck,downCheck;
    private int upRatio;
    [SerializeField] TMP_Text remainTime;
    
    #region singleton
    public static Clock clockInstance;
    private void Awake() 
    {
        timers = new Dictionary<string, float>();
        clockInstance = this;
        up = new Vector2(0,6);
        down = new Vector2(0,5);  
    }
    #endregion
    void FixedUpdate()
    {
        List<string> tem = new List<string>();
        if(timers != null)
        {
            foreach(var timer in timers) tem.Add(timer.Key);
            for(int i=0;i<tem.Count;i++) 
            {
                timers[tem[i]] = timers[tem[i]] - Time.fixedDeltaTime;
                if(timers[tem[i]]<=0) timers.Remove(tem[i]);
            }
        }

        #region spellCardCounter
        if(spellCardCount)
        {
            if(spellCardTimer - Time.fixedDeltaTime<=0)
            {
                spellCardTimer = 0f;
                spellCardCount = false;
            }
            else spellCardTimer -= Time.fixedDeltaTime;
            remainTime.text = spellCardTimer.ToString("f2");
        }
        #endregion
    }
    public void setTimer(string timerName,float seconds)
    {
        if(timers==null) timers = new Dictionary<string, float>();
        timers.Add(timerName,seconds);
    }
    public bool checkTimer(string timerName)
    {
        return timers.ContainsKey(timerName);
    }
    public void setSpellCardTimer(float time)
    {
        remainTime.enabled = true;
        spellCardTimer = time;
        remainTime.text = spellCardTimer.ToString("f2");
        spellCardCount = true;
    } 
    public void concellSpellCardTimer()
    {
        remainTime.enabled = false;
        spellCardTimer = 0f;
        spellCardCount = false;
    }
}
