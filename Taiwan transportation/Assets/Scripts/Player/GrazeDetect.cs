using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrazeDetect : MonoBehaviour
{
    static int grazeCount;
    [SerializeField] AudioSource grazeSound;
    
    private void Start(){
        grazeCount = 0;
        refreshGrazeText();
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "enemy_bullet"){
            if(other.GetComponent<EnemyBullet>().isGrazed == false){
                other.GetComponent<EnemyBullet>().isGrazed = true;
                grazeCount ++;
                grazeSound.Play();
                refreshGrazeText();
            }
        }
    }

    public static int getGrazeCount(){
        return grazeCount;
    }

    void refreshGrazeText(){
        StageObj.StageTexts["graze"].GetComponent<Text>().text = "Graze: " + grazeCount;
    }
}
