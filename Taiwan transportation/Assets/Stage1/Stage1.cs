using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Stage1 : MonoBehaviour
{
    [SerializeField] float summonTime;
    [SerializeField] TMP_Text time_text;
    public float stageTimer;
    
    void Start(){
        stageTimer = 0;
        StartCoroutine(startStage());
    }
    void Update(){
        stageTimer += Time.deltaTime;
        if(stageTimer < 10){
            time_text.text = "00" + (int)stageTimer;
        }
        else if(stageTimer < 100){
            time_text.text = "0" + (int)stageTimer;
        }
        else{
            time_text.text = "" + (int)stageTimer;
        }
    }

    IEnumerator startStage(){
        yield return new WaitForSeconds(summonTime);
        GameObject enemy;
        YieldInstruction delayTime, delayOneSec;
        delayOneSec = new WaitForSeconds(1);

        delayTime = new WaitForSeconds(1f);
        for(int i=4; i>=0; i--){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(0, -2));
            enemy.GetComponent<NormalScooter>().setShootSector(new Vector2(0, -2f), 5, 60f, false, 0.7f, 0.5f, 1);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, -90f);
            enemy.GetComponent<NormalScooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.3f+i*0.1f, 0.7f, 3);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(1f);
        for(int i=0; i<=4; i++){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(0, -2));
            enemy.GetComponent<NormalScooter>().setShootSector(new Vector2(0, -2f), 5, 60f, false, 0.7f, 0.5f, 1);
            yield return delayTime;
        }
        yield return delayOneSec;
        
        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, 90f);
            enemy.GetComponent<NormalScooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.3f+i*0.1f, 0.7f, 3);
            yield return delayTime;
        }
        yield return delayOneSec;
        yield return delayOneSec;
        

        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setStraightMove(new Vector2(0, -4));
            enemy.GetComponent<NormalScooter>().setShootSector(new Vector2(0, -2f), 5, 100f, true, 0.5f, 1f, 1);
            yield return delayTime;
        }
        yield return delayTime;
        for(int i=4; i>=0; i--){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-4 +i*2f , 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setStraightMove(new Vector2(0, -4));
            enemy.GetComponent<NormalScooter>().setShootSector(new Vector2(0, -2f), 5, 100f, true, 0.5f, 1f, 1);
            yield return delayTime;
        }
        yield return delayOneSec;
        yield return delayOneSec;

        delayTime = new WaitForSeconds(2);
        for(int i=0; i<2; i++){
            enemy = Instantiate(StageObj.Enemies["red_car"], new Vector2(-3 +6*i, 7f), transform.rotation);
            enemy.GetComponent<NormalCar>().setStraightMove(new Vector2(0, -2));
            enemy.GetComponent<NormalCar>().setShootCircle(new Vector2(0, -1.5f), 11, true, 0.7f, 1f, 1);
        }
        yield return delayTime;

        delayTime = new WaitForSeconds(0.5f);
        for(int i=0; i<7; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, -90f);
            enemy.GetComponent<NormalScooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.5f, 1f +0.2f*i, 1);
            
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, 90f);
            enemy.GetComponent<NormalScooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 1, true, 0.5f, 1f +0.2f*i, 1);
            yield return delayTime;
        }

    }
}
