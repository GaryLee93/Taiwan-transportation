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


}
