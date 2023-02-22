using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_move : MonoBehaviour
{
    public float speed=0.1f;
    public Rigidbody2D rb;
    void Start()
    {
       rb.velocity = new Vector2(0,speed);
    }

    void OnTriggerEnter2D()
    {
        Destroy(gameObject);
    }
}
