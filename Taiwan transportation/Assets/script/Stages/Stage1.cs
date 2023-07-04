using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1 : MonoBehaviour
{
    [SerializeField]float summonTime;
    [SerializeField]GameObject[] enemies;
    
    Vector2 speedVector;
    int accelType;
    int shoot_angle_diff;
    Vector2 accelVector;
    
    const float RIGHTBORDER = 6.2f;
    const float LEFTBORDER = -6.2f;
    const float UPPERBORDER = 7.2f;
    const float LOWERBORDER = -7.2f;
    public string next_stage = "Stage_2";
    
    void Start(){
        StartCoroutine(startStage());
        if(Input.GetKey(KeyCode.N)){
            SceneManager.LoadScene(next_stage);
        }
    }

    IEnumerator startStage(){
        yield return new WaitForSeconds(1f);
        StartCoroutine(summonMonster());
    }

    IEnumerator summonMonster(){
        GameObject enemy;
        enemy = Instantiate(enemies[0], new Vector2(-3f, 5f), transform.rotation);
        enemy.GetComponent<ModelMovement>().setMovement(1, new Vector2(0, -2), 1);

        enemy.GetComponent<ModelMovement>().setAccel(0, new Vector2(0.1f, 0), 1f);
        enemy.GetComponent<ModelMovement>().setAccel(1, new Vector2(0.1f, 0), 3f);
        
        yield return new WaitForSeconds(0.5f);
        /*
        for(int i=0; i<5; i++){
            speedVector = new Vector2(0f, -2f);
            
            enemy = spawnEnemy(0, 1, 1, new Vector3(3.5f, 7.2f, 0f));
            modifyEnemyAccel(enemy, 1, new Vector2(-0.1f - i*.01f, 0f));
            enemy.GetComponent<EnemyBehavior>().shoot_angle_diff = 10;

            enemy = spawnEnemy(0, 1, 1, new Vector3(4.5f, 7.2f, 0f));
            modifyEnemyAccel(enemy, 1, new Vector2(-0.1f - i*.01f, 0f));
            enemy.GetComponent<EnemyBehavior>().shoot_angle_diff = 10;

            yield return new WaitForSeconds(0.5f);
        }
        
        for(int i=0; i<5; i++){
            speedVector = new Vector2(0f, -2f);

            enemy = spawnEnemy(0, 1, 1, new Vector3(-3.5f, 7.2f, 0f));
            modifyEnemyAccel(enemy, 1, new Vector2(0.1f + i*.01f, 0f));
            enemy.GetComponent<EnemyBehavior>().shoot_angle_diff = 10;

            enemy = spawnEnemy(0, 1, 1, new Vector3(-4.5f, 7.2f, 0f));
            modifyEnemyAccel(enemy, 1, new Vector2(0.1f + i*.01f, 0f));
            enemy.GetComponent<EnemyBehavior>().shoot_angle_diff = 10;

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(2f);

        for(int i=-5; i<=5; i++){
            speedVector = new Vector2(0f, -2f);
            spawnEnemy(0, 1, 1, new Vector3(-i*0.8f, 7.2f, 0f));
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i=-4; i<=4; i++){
            speedVector = new Vector2(0f, -2f);
            spawnEnemy(0, 1, 2, new Vector3(i, 7.2f, 0f));
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i=-5; i<=5; i++){
            speedVector = new Vector2(0f, -2f);
            spawnEnemy(0, 1, 1, new Vector3(-i*0.8f, 7.2f, 0f));
            yield return new WaitForSeconds(0.05f);
        }*/

    }
    void modifyEnemyAccel(GameObject enemy, int accelType, Vector2 accelVector){
        enemy.GetComponent<EnemyBehavior>().accelType = 1;
        enemy.GetComponent<EnemyBehavior>().accelVector = accelVector;
    }
    GameObject spawnEnemy(int enemyType, int moveType, int shootType, Vector3 enemySpawnPos){
        GameObject tmpEnemy = Instantiate(enemies[enemyType], enemySpawnPos, transform.rotation);
        tmpEnemy.GetComponent<EnemyBehavior>().moveType = moveType;
        tmpEnemy.GetComponent<EnemyBehavior>().shootType = shootType;
        tmpEnemy.GetComponent<EnemyBehavior>().speedVector = speedVector;
        
        return tmpEnemy;
    }
}
