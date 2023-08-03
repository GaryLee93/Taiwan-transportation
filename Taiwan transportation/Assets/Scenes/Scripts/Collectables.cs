using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    public enum ColType{ Power, BigPower, Score, OneUP, Bomb}
    public ColType Type;
    static float collectRange = 3f;
    static float collectSpeed = 10f;
    static float collect_line_height = 3f;
    bool isCollected;
    Rigidbody2D rb;
    Transform playerTF;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        playerTF = Player.GetPlayer().transform;

        isCollected = false;
    }
    void Update()
    {
        suck();
        if(hitBorder())
            Destroy(gameObject);
    }
    void suck(){
        if(Player.GetPlayer().GetComponent<Rigidbody2D>().simulated == true){
            Vector2 diff = (Vector2)playerTF.position-(Vector2)transform.position;
            float dis = diff.sqrMagnitude;
            diff.Normalize();
            if(dis < collectRange || playerTF.position.y > collect_line_height)
            {
                isCollected = true;
            }

            if(isCollected)
            {
                rb.velocity=diff*collectSpeed;
            }
        }
        else if(isCollected){
            isCollected = false;
            rb.velocity = Vector2.zero;
        }
    }

    bool hitBorder(){   //no upper bound
        const float XBORDER = 5.9f, YBORDER = 6.8f;
        return this.transform.position.x > XBORDER || this.transform.position.x < -XBORDER || 
                this.transform.position.y < -YBORDER ;
    }
}
