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

        delayTime = new WaitForSeconds(1f);
        for(int i=0; i<3; i++){
            enemy = Instantiate(StageObj.Enemies["scooter"], new Vector2(-4 +i*4f , 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setStopMove(new Vector2(0, -3), 1, 3, new Vector2(0, -2));
            yield return delayTime;
        }
        yield return delayOneSec;

        delayTime = new WaitForSeconds(0.4f);
        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["scooter"], new Vector2(-4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, 90f);
            yield return delayTime;
        }
        yield return delayOneSec;

        for(int i=0; i<5; i++){
            enemy = Instantiate(StageObj.Enemies["scooter"], new Vector2(4, 7f), transform.rotation);
            enemy.GetComponent<NormalScooter>().setTurnCircle(new Vector2(0, -3f), 2f, 0.5f, -90f);
            yield return delayTime;
        }
        yield return delayOneSec;

    }
}
