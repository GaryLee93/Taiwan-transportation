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
    bool w=true;
    bool t=true;

    void Start()
    {
        flowerSys = FlowerManager.Instance.CreateFlowerSystem("FlowerSample",false);
        //flowerSys.SetupDialog();

        // Setup Variables.
        myName = "Rempty (｢･ω･)｢";
        flowerSys.SetVariable("MyName", myName);

        // Define your customized commands.
        flowerSys.RegisterCommand("UsageCase", CustomizedFunction);
        // Define your customized effects.
        flowerSys.RegisterEffect("customizedRotation", EffectCustomizedRotation);
        clock = Clock.clockInstance;
    }

    void Update()
    {
        // ----- Integration DEMO -----
        // Your own logic control.
        if(pass.isPlaying&&w)
        {
            if(t)
            {
                clock.setTimer("dialog",7f);
                t=false;
            }
            if(!clock.checkTimer("dialog")) 
                {
                    flowerSys.SetupDialog();
                    progress=0;
                    w=false;
                }
        }
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
            if(Input.GetKeyDown(KeyCode.Space)){
                // Continue the messages, stoping by [w] or [lr] keywords.
                flowerSys.Next();
            }
            if(Input.GetKeyDown(KeyCode.R)){
                // Resume the system that stopped by [stop] or Stop().
                flowerSys.Resume();
            }
        }
    }

    private void CustomizedFunction(List<string> _params)
    {
        var resultValue = int.Parse(_params[0]) + int.Parse(_params[1]);
        Debug.Log($"Hi! This is called by CustomizedFunction with the result of parameters : {resultValue}");
    }
    
    IEnumerator CustomizedRotationTask(string key, GameObject obj, float endTime){
        Vector3 startRotation = obj.transform.eulerAngles;
        Vector3 endRotation = obj.transform.eulerAngles + new Vector3(0,180,0);
        // Apply default timer Task.
        yield return flowerSys.EffectTimerTask(key, endTime, (percent)=>{
            // Update function.
            obj.transform.eulerAngles = Vector3.Lerp(startRotation, endRotation, percent);
        });
    }

    private void EffectCustomizedRotation(string key, List<string> _params){
        try{
            // Parse parameters.
            float endTime;
            try{
                endTime = float.Parse(_params[0])/1000;
            }catch(Exception e){
                throw new Exception($"Invalid effect parameters.\n{e}");
            }
            // Extract targets.
            GameObject sceneObj = flowerSys.GetSceneObject(key);
            // Apply tasks.
            StartCoroutine(CustomizedRotationTask($"CustomizedRotation-{key}", sceneObj, endTime));
        }catch(Exception){
            Debug.LogError($"Effect - SpriteAlphaFadeIn @ [{key}] failed.");
        }
    }
}
