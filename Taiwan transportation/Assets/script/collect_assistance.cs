using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect_assistance : MonoBehaviour
{
    public float range;
    public float speed;
    public float collect_line;
    private Rigidbody2D rb;
    private Transform player;
    void Start()
    {
        rb=gameObject.GetComponent<Rigidbody2D>();
        player=GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        restrict();
    }
    
    void restrict()
    {
        Vector2 diff=(Vector2)player.position-(Vector2)transform.position;
        float dis=diff.sqrMagnitude;
        diff.Normalize();
        if(dis<range)
        {
            rb.velocity=diff*speed;
        }
        else if(player.position.y>collect_line)
        {
            rb.velocity=diff*speed*5;
        }

    }
}
