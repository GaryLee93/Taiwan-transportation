using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    private float tem_speed=0f;
    public float speed=0.035f;
    public float slow_speed=0.002f;
    public GameObject check_point;
    public GameObject childPlane_0,childPlane_1,childPlane_2,childPlane_3;
    [SerializeField]private Rigidbody2D rb;
    void Update()
    {
        if(Input.GetKey(KeyCode.RightShift)||Input.GetKey(KeyCode.LeftShift))
        {
            slow_mod();
        }
        else
        {
            normal_mod();
        }
    }

    void move()
    {
        float hori=Input.GetAxisRaw("Horizontal");
        float ver=Input.GetAxisRaw("Vertical");
        if(hori!=0 && ver!=0){
            hori/=1.41421356237f;
            ver/=1.41421356237f;
        }
        
        rb.velocity = new Vector2(hori*tem_speed, ver*tem_speed);
    }
    void normal_mod()
    {
        check_point.SetActive(false);
        childPlane_0.transform.localPosition= new Vector3(1f,-1f,0f);
        childPlane_1.transform.localPosition= new Vector3(-1f,-1f,0);
        childPlane_2.transform.localPosition= new Vector3(1.75f,-0.25f,0);
        childPlane_3.transform.localPosition= new Vector3(-1.75f,-0.25f,0);
        tem_speed=speed;
        move();
    }
    void slow_mod()
    {
        check_point.SetActive(true);
        childPlane_0.transform.localPosition= new Vector3(0.75f,-0.75f,0f);
        childPlane_1.transform.localPosition= new Vector3(-0.75f,-0.75f,0);
        childPlane_2.transform.localPosition= new Vector3(1.25f,-0.25f,0);
        childPlane_3.transform.localPosition= new Vector3(-1.25f,-0.25f,0);
        tem_speed=slow_speed;
        move();
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag=="enemy"||other.gameObject.tag=="enemy_bullet")
        {
            die();
        }
    }
    void die()
    {
        Debug.Log("滿身瘡痍");
    }
}
