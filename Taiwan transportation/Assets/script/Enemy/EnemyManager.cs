using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]float summonTime;
    [SerializeField]GameObject[] enemy;
    
    Vector2 moveVector;
    int enemyType;
    const float RIGHTBORDER = 6.2f;
    const float LEFTBORDER = -6.2f;
    const float UPPERBORDER = 7.2f;
    const float LOWERBORDER = -7.2f;
    void Start(){
        StartCoroutine(stageStart());
    }

    IEnumerator stageStart(){
        Vector3 tmp_spawnPos;
        
        for(int i=0; i<5; i++){
            tmp_spawnPos = new Vector3(RIGHTBORDER, 6f - i, 0f);
            moveVector = new Vector2(-1f, 0f);
            spawnEnemy(1, tmp_spawnPos, 1, 1);
            
        }
        yield return new WaitForSeconds(3f);

        for(int i=0; i<6; i++){
            tmp_spawnPos = new Vector3(LEFTBORDER + 1.5f*i, 6.5f, 0f);
            moveVector = new Vector2(1f, 0f);
            spawnEnemy(0, tmp_spawnPos, 2, 1);
            
            yield return new WaitForSeconds(0.5f);
        }

        
        
    }

    void spawnEnemy(int ememyType, Vector3 spawnPos, int moveType, int shootType){
        GameObject tmpEnemy = Instantiate(enemy[enemyType], spawnPos, transform.rotation);
        tmpEnemy.GetComponent<EnemyBehavior>().moveType = moveType;
        tmpEnemy.GetComponent<EnemyBehavior>().shootType = shootType;
        if(moveType == 1)
            tmpEnemy.GetComponent<EnemyBehavior>().moveVector = moveVector;
        
    }
}
