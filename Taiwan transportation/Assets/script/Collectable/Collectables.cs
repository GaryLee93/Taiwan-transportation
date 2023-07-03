using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum ColType{
        Power,
        Score,
        OneUP,
        Bomb
    }
    public ColType Type;
    [SerializeField] float range = 3f;
    [SerializeField] float speed = 10f;
    [SerializeField] float collect_line_height = 3f;
    private Rigidbody2D rb;
    private Transform player;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
        else if(player.position.y > collect_line_height)
        {
            rb.velocity=diff*speed;
        }
    }

    bool hitBorder(){
        const float XBORDER = 6.6f, YBORDER = 7.5f;
        return this.transform.position.x < -XBORDER ||
                this.transform.position.y > YBORDER || this.transform.position.y < -YBORDER ;
    }
}
