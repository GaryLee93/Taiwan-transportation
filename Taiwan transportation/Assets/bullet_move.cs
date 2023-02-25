using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move : MonoBehaviour,Ipooled
{
    public float speed=0.1f;
    public Rigidbody2D rb;
    public void onBulletSpawn()
    {
       rb.velocity = new Vector2(0,speed);
    }
}
