using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class be_collected : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag=="Player")
        {
            player_script player=other.GetComponent<player_script>();
            if(gameObject.name=="1UP")
            {
                player.palyer_num++;
            }
            else if(gameObject.name=="bomb")
            {
                player.bomb_num++;
            }
            else if(gameObject.name=="power")
            {
                Debug.Log("collision");
                player.power+=0.05f;
            }
            else if(gameObject.name=="Taiwan_credit")
            {
                player.score++;
            }
            Destroy(gameObject);
        }
    }
}
