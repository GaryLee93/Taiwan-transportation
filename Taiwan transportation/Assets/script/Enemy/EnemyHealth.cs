using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]private int health = 1000;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "polyball"){
            health -= 1;
        }
        if(other.tag == "wrench"){
            health -= 5;
        }
        if(health <= 0){
            Destroy(gameObject);
        }
    }
}
