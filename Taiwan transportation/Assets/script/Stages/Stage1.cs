using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Stage1 : MonoBehaviour
{
    [SerializeField] float summonTime;
    [SerializeField] TMP_Text time_text;
    public string next_stage = "Stage_2";
    public float stageTimer;
    
    void Start(){
        stageTimer = 0;
        StartCoroutine(startStage());
        if(Input.GetKey(KeyCode.N)){
            //SceneManager.LoadScene(next_stage);
        }
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

        delayTime = new WaitForSeconds(1.5f);
        for(int i=2; i>=0; i--){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*4f , 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(0, -2));
            enemy.GetComponent<NormalScooter>().setShootSector(new Vector2(0, -1.5f), 5, 60f, false, 0.4f, 0.2f, 5);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(0.4f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, -90f);
            enemy.GetComponent<NormalScooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 3, true, 0.3f, 1f, 3);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(1.5f);
        for(int i=0; i<3; i++){
            enemy = Instantiate(StageObj.Enemies["red_scooter"], new Vector2(-4 +i*4f , 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setStopMove(new Vector2(0, -3), 1, 2, new Vector2(0, -2));
            enemy.GetComponent<NormalScooter>().setShootSector(new Vector2(0, -1.5f), 5, 60f, false, 0.4f, 0.2f, 5);
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(0.4f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["blue_scooter"], new Vector2(-4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, 90f);
            enemy.GetComponent<NormalScooter>().setShootCircle(
                ourTool.vectorToPlayer(enemy).normalized *3, 3, true, 0.3f, 1f, 3);
            yield return delayTime;
        }
        yield return delayOneSec;

    }
}
