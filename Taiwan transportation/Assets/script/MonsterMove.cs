using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity=new Vector2(speed,0);
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag=="border")
        {
            speed*=-1;
            rb.velocity=new Vector2(speed,0);
            Debug.Log("collision");
        }
    }
}
