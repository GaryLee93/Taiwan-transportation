using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private Dictionary<string,float> timers;
    
    #region singleton
    public static Clock clockInstance;
    private void Awake() 
    {
        timers = new Dictionary<string, float>();
        clockInstance = this;    
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

}
