using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move : MonoBehaviour
{
    public float speed=0.1f;
    public Rigidbody2D rb;
    public void Update()
    {
       rb.velocity = new Vector2(0,speed);
    }
}
