using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backMirrorBehavior : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] GameObject glass;
    AudioSource splashSound;
    objectPooler objInstance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        splashSound = GetComponent<AudioSource>();
        objInstance = objectPooler.instance;
        transform.Rotate(new Vector3(0f, 0f, Random.Range(0f, 180f)));
    }
    void Update(){
        if(hitBorder()) 
            Destroy(gameObject);
    }
    bool hitBorder(){
        if(Mathf.Abs(transform.position.x) > 7.5f || Mathf.Abs(transform.position.y) > 7.5f) 
            return true;
        else 
            return false;
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag == "border")
        {
            bool bounce = false;
            float new_x = rb.velocity.x,new_y = rb.velocity.y;
            if(other.gameObject.name == "upWall") 
            {
                bounce = true;
                new_y *= -1;
            }
            if(other.gameObject.name == "leftWall" || other.gameObject.name == "rightWall") 
            {
                bounce = true;
                new_x *= -1;
            }
            
            if(bounce)
            {
                splashSound.Play();
                transform.Rotate(new Vector3(0f, 0f, Random.Range(60f, 120f)));
                rb.velocity = new Vector2(new_x,new_y);
                GameObject shooter = transform.GetChild(0).gameObject;
                GameObject colone;
                shooter.transform.localPosition = new Vector3(0,0,0);
                Vector2 dire = new Vector2(1,0);
                
                for(int i=0;i<=15;i++)
                {
                    shooter.transform.Rotate(new Vector3(0,0,(360/20)));
                    colone = objectPooler.spawnFromPool("glass",transform.position,transform.rotation);
                    colone.transform.Rotate(new Vector3(0,0,Random.Range(0,360)));
                    colone.GetComponent<Rigidbody2D>().velocity = dire*Random.Range(3f, 5f);
                    dire = trans_matrix(dire,eulerToRadian(360/20));
                }
            }
        }
    }
    Vector2 trans_matrix(Vector2 orign,float radian)
    {
        float new_a = orign.x*Mathf.Cos(radian) - orign.y*Mathf.Sin(radian);
        float new_b = orign.x*Mathf.Sin(radian) + orign.y*Mathf.Cos(radian);
        return new Vector2(new_a,new_b);
    }
    float eulerToRadian(float euler)
    {
        return (euler/180)*Mathf.PI;
    }
}
