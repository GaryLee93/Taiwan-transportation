using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]float summonTime;
    [SerializeField]GameObject[] enemy;
    
    Vector2 moveVector;
    int enemyType;
    void Start(){
        StartCoroutine(stageStart());
    }

    IEnumerator stageStart(){
        Vector3 tmp_spawnPos;
        
        for(int i=0; i<5; i++){
            tmp_spawnPos = new Vector3(3.4f, 6f - i, 0f);
            moveVector = new Vector2(-1f, 0f);
            spawnEnemy(tmp_spawnPos, 1, 1);
            
        }
        

        yield return new WaitForSeconds(2f);

        for(int i=0; i<5; i++){
            tmp_spawnPos = new Vector3(-9f, 6f - i, 0f);
            moveVector = new Vector2(-1f, 0f);
            spawnEnemy(tmp_spawnPos, 2, 1);
        }

        yield return new WaitForSeconds(2f);

        for(int i=0; i<5; i++){
            tmp_spawnPos = new Vector3(3.4f, 6f - i, 0f);
            moveVector = new Vector2(-1f, -i +0f);
            spawnEnemy(tmp_spawnPos, 1, 1);
            yield return new WaitForSeconds(0.2f);
        }
    }

    void spawnEnemy(Vector3 spawnPos, int moveType, int shootType){

        GameObject tmpEnemy = Instantiate(enemy[0], spawnPos, transform.rotation);
        tmpEnemy.GetComponent<EnemyBehavior>().moveType = moveType;
        tmpEnemy.GetComponent<EnemyBehavior>().shootType = shootType;
        tmpEnemy.GetComponent<EnemyBehavior>().moveVector = moveVector;
        
    }
}
