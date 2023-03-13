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
        suck();
        if(hitBorder())
            Destroy(gameObject);
    }
    
    void suck()
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

    bool hitBorder(){
        const float XBORDER = 6.6f, YBORDER = 7.5f;
        return this.transform.position.x < -XBORDER || this.transform.position.x > XBORDER || 
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
