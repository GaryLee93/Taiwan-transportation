using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class test : MonoBehaviour
{
    Clock clock;
    public VideoPlayer vp;
    public VideoPlayer b;
    private bool t=true;
    private bool f,check=false;
    // Start is called before the first frame update
    void Start()
    {
        clock = Clock.clockInstance;
        vp.Prepare();
        vp.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if(b.isPlaying)
        {
            check=true;
        }
        if(!b.isPlaying&&t&&check)
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
