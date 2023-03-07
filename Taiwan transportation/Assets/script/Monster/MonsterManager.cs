using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    [SerializeField]float summonTime;
    [SerializeField]GameObject monster;
    void Start(){
        for(int i=0; i<10; i++){
            Invoke("spawnMonster", summonTime + i*0.5f);
        }
    }

    void spawnMonster(){
        Vector3 spawnPos = new Vector3(3.4f, 6f, 0f);
        Instantiate(monster, spawnPos, transform.rotation);
        spawnPos = new Vector3(3.4f, 4f, 0f);
        Instantiate(monster, spawnPos, transform.rotation);
    }
}
