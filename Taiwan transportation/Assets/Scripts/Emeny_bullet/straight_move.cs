using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straight_move : MonoBehaviour,Ipooled
{
    public void onBulletSpawn()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = 3*transform.localPosition.normalized;
        gameObject.transform.parent = null;
    }
    public void poolDespawn(){
        gameObject.SetActive(false);
    }
}
