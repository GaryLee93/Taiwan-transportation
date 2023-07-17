using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class changefield : MonoBehaviour
{
    Clock clock;
    public VideoPlayer vp;
    public GameObject b;
    public string num;
    private bool t=true;
    private bool f=false;
    void Start()
    {
        vp.Prepare();
        vp.Stop();
        clock = Clock.clockInstance;
    }
    void Update()
    {
        if(t && Input.GetKey(num))
        {
            t=false;
            vp.Play();
            clock.setTimer("flame",0.1f); 
            f=true;
        }
        if(f && !clock.checkTimer("flame"))
        {
            this.transform.Rotate(0,-90,0);
            b.transform.Rotate(0,90,0);
            f=false;
        }
    }
}
