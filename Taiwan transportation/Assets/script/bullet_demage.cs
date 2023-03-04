using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_demage : MonoBehaviour
{
    public float demage;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        MonsterMove mv=other.GetComponent<MonsterMove>();
        if(mv!=null)
        {
            mv.takeDemage(demage);
        }
    }
}
