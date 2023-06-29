using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]float summonTime;
    [SerializeField]GameObject[] enemy;
    
    Vector2 speedVector;
    Vector3 enemySpawnPos;
    int enemyType;
    const float RIGHTBORDER = 6.2f;
    const float LEFTBORDER = -6.2f;
    const float UPPERBORDER = 7.2f;
    const float LOWERBORDER = -7.2f;

    IEnumerator startStage(){

        yield return new WaitForSeconds(1f);

        StartCoroutine(summonMonster());
    }

    IEnumerator summonMonster(){
        yield return new WaitForSeconds(1f);
               
    }

    void spawnEnemy(int ememyType, int moveType, int shootType){
        GameObject tmpEnemy = Instantiate(enemy[enemyType], enemySpawnPos, transform.rotation);
        tmpEnemy.GetComponent<EnemyBehavior>().moveType = moveType;
        tmpEnemy.GetComponent<EnemyBehavior>().shootType = shootType;
        tmpEnemy.GetComponent<EnemyBehavior>().speedVector = speedVector;
        
    }
}
