using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_recycle : MonoBehaviour
{
    private void Update() {
        if(hitBorder())
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="enemy")
        {
            gameObject.SetActive(false);
        }
    }

    private bool hitBorder(){
        return this.transform.position.x <-8.7 || this.transform.position.x > 3 || 
                this.transform.position.y > 7 || this.transform.position.y < -7 ;
    }
}
