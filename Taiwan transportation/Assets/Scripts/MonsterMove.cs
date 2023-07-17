using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] float HP=200f;
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
        }
    }

    public void takeDemage(float demage)
    {
        HP-=demage;
        if(HP<=0)
        {
            death();
        }
    }

    void death()
    {
        Destroy(gameObject);
    }
}
