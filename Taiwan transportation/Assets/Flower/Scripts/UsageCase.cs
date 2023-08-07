using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Flower;

public class UsageCase : MonoBehaviour
{
    FlowerSystem flowerSys;
    Clock clock;
    private string myName;
    private int progress = 10;
    public bool isGameEnd = false;
    public VideoPlayer pass;
    bool w=false;
    bool t=true;

    void Start()
    {
        flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample",false);
        //flowerSys.SetupDialog();

        // Setup Variables.
        myName = "Rempty (｢･ω･)｢";
        flowerSys.SetVariable("MyName", myName);
        clock = Clock.clockInstance;
    }

    void Update()
    {   
        if(flowerSys.isCompleted && !isGameEnd ){
            switch(progress){
                case 0:
                    flowerSys.ReadTextFromResource("bang");
                    progress ++;                  
                    break;
                case 1:
                    isGameEnd=true;
                    progress ++;
                    break;
                }
            
        }

        if (!isGameEnd)
        {
            if(Input.GetKeyDown(KeyCode.Z)){
                // Continue the messages, stoping by [w] or [lr] keywords.
                flowerSys.Next();
            }
            if(Input.GetKeyDown(KeyCode.R)){
                // Resume the system that stopped by [stop] or Stop().
                flowerSys.Resume();
            }
        }
    }

    public void startDialogue(){
        flowerSys.SetupDialog();
        progress=0;
    }
}
    
    