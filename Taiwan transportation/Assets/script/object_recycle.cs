using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_recycle : MonoBehaviour
{
    private void Update() {
        if(hitBorder())
            gameObject.SetActive(false);
    }
    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="enemy")
        {
            gameObject.SetActive(false);
        }
    }
    */

    private bool hitBorder(){
        const float XBORDER = 6.7f, YBORDER = 7.5f;
        return this.transform.position.x <-XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
