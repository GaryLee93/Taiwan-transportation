using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming_player : MonoBehaviour,Ipooled
{
    public float velocity;
    Rigidbody2D rb;
    public void onBulletSpawn()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        GameObject target = GameObject.FindGameObjectWithTag("Player");
        Vector3 dire = (target.transform.position - transform.position);
        dire.Normalize();
        rb.velocity = dire*velocity;
    }
    public void setParent(GameObject newParent)
    {
        transform.parent = newParent.transform;
    }

    public void poolDespawn(){
        gameObject.SetActive(false);
    }
}
