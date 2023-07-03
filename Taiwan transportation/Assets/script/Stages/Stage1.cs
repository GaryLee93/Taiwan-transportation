using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Stage1 : MonoBehaviour
{
    [SerializeField] float summonTime;
    [SerializeField] GameObject[] enemy_arr;
    [SerializeField] TMP_Text time_text;
    const float RIGHTBORDER = 6.2f;
    const float LEFTBORDER = -6.2f;
    const float UPPERBORDER = 7.2f;
    const float LOWERBORDER = -7.2f;
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

        if(Input.GetKey(KeyCode.P)){
            Time.timeScale = 0;
        }

        if(Input.GetKey(KeyCode.R)){
            Time.timeScale = 1;
        }
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
        yield return new WaitForSeconds(1f);
        StartCoroutine(summonMonster());
    }

    IEnumerator summonMonster(){
        GameObject enemy;

        enemy = spawnEnemy(new Vector2(0f, 7.2f), 0, 0);
        pop_from_top(enemy);
        modifyAccel(enemy, 0, new Vector2(), 2);
        modifyAccel(enemy, 1, new Vector2(0f, -0.2f), 1);
        modifyShoot(enemy, 2, 1, Vector2.down*2, 2f, 0);
        modifyShootTime(enemy, 0.5f, 5f);
        enemy.transform.GetChild(0).GetComponent<BulletShooterModel>().sector_angle = 20f;
        
        yield return new WaitForSeconds(1f);
        
        for(int i=0; i<2; i++){
            enemy = spawnEnemy(new Vector2(-3f + i*6, 7f), 0, 0);
            pop_from_top(enemy);
            modifyAccel(enemy, 0, new Vector2(), 2);
            modifyAccel(enemy, 1, new Vector2(0f, -0.2f), 1);
            modifyShoot(enemy, 2, 1, Vector2.down*2, 2f, 0);
            modifyShootTime(enemy, 0.5f, 5f);
            enemy.transform.GetChild(0).GetComponent<BulletShooterModel>().sector_angle = 20f;
        }
        yield return new WaitForSeconds(3f);

        for(int i=0; i<4; i++){
            enemy = spawnEnemy(new Vector2(-4.5f + i*3, 7f), 0, 0);
            pop_from_top(enemy);
            modifyMove(enemy, 1, new Vector2(Random.Range(-2, 2)/2f, -1f), 1);
            modifyShoot(enemy, 2, 1, Vector2.down*1.5f, 3f, 0);
            modifyShootTime(enemy, 0.5f, 5f);
            enemy.transform.GetChild(0).GetComponent<BulletShooterModel>().sector_angle = 10f;
        }
        yield return new WaitForSeconds(1f);

        for(int i=0; i<8; i++){
            enemy = spawnEnemy(new Vector2(-4.2f + i*1.2f, 7f), 0, 0);
            pop_from_top(enemy);
            modifyMove(enemy, 1, new Vector2(Random.Range(-1f, 1f), -1), 1);
            modifyShoot(enemy, 2, 1, Vector2.down*1.5f, 3f, 0);
            modifyShootTime(enemy, 0.5f, 5f);
        }
        yield return new WaitForSeconds(3f);

        for(int i=0; i<5; i++){
            enemy = spawnEnemy(new Vector2(6.2f, 5f), 0, 0);
            modifyMove(enemy, 1, new Vector2(-2f, 0), 1);
            modifyShoot(enemy, 2, 1, Vector2.down*1.2f, 2f, 0);
            enemy.transform.GetChild(0).GetComponent<BulletShooterModel>().shoot_at_player = true;
            modifyShootTime(enemy, 0.5f, 10f);
            yield return new WaitForSeconds(0.5f);
        }


    }
    GameObject spawnEnemy(Vector3 enemySpawnPos, int enemyType, int shootType){
        GameObject tmpEnemy = Instantiate(enemy_arr[enemyType], enemySpawnPos, transform.rotation);
        tmpEnemy.GetComponent<EnemyBehavior>().shootType = shootType;
        tmpEnemy.GetComponent<ModelMovement>().moveList = new List<ModelMovement.fumoType>();
        tmpEnemy.GetComponent<ModelMovement>().accelList = new List<ModelMovement.fumoType>();
        return tmpEnemy;
    }
    void modifyMove(GameObject enemy, int type, Vector2 direction, float time){
        List<ModelMovement.fumoType> mList = enemy.GetComponent<ModelMovement>().moveList;
        mList.Add(new ModelMovement.fumoType(1, direction, time));
    }
    void modifyAccel(GameObject enemy, int type, Vector2 direction, float time){
        List<ModelMovement.fumoType> accList = enemy.GetComponent<ModelMovement>().accelList;
        accList.Add(new ModelMovement.fumoType(1, direction, time));
    }

    void modifyShoot(GameObject enemy, int type, int count, Vector2 direction, float interval, float angle_diff){
        BulletShooterModel tmp_bsm = enemy.transform.GetChild(0).GetComponent<BulletShooterModel>();
        tmp_bsm.shoot_type = type;
        tmp_bsm.shoot_count = count;
        tmp_bsm.shoot_direction = direction;
        tmp_bsm.shoot_interval = interval;
        tmp_bsm.shoot_angle_diff = angle_diff;
    }
    void modifyShootTime(GameObject enemy, float start, float end){
        BulletShooterModel tmp_bsm = enemy.transform.GetChild(0).GetComponent<BulletShooterModel>();
        tmp_bsm.start_time = start;
        tmp_bsm.end_time = end;
    }
    void pop_from_top(GameObject enemy){
        modifyMove(enemy, 1, new Vector2(0f, -2.5f), 1);
        modifyAccel(enemy, 0, new Vector2(), 0.5f);
        modifyAccel(enemy, 1, new Vector2(0f, 0.5f), 0.5f);
    }
}
