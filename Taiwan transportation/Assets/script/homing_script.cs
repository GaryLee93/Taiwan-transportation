using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class homing_script  : MonoBehaviour
{
    public string target_tag;
    public float speed=5f;
    public float rotateSpeed=200f;
    private Rigidbody2D rb;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate() 
    {
        homing();
    }
    public void homing()
    {
        Transform target=find_closest_target().transform;
        if(target==null) 
        {
            Debug.Log("no target");
            return ;
        }
        Vector2 direction=(Vector2)target.position-rb.position;
        direction.Normalize();
        float rotateAmount=Vector3.Cross(direction,transform.up).z;
        rb.angularVelocity = -rotateAmount*rotateSpeed;
        rb.velocity=transform.up*speed;
    }
    GameObject find_closest_target()
    {
        GameObject[] targets=GameObject.FindGameObjectsWithTag(target_tag);
        GameObject closest_target=null;
        float distance=Mathf.Infinity;
        Vector2 position=transform.position;
        foreach(GameObject target in targets)
        {
           Vector2 diff=(Vector2)target.transform.position-position;
           float tem_dis=diff.sqrMagnitude;
           if(tem_dis<distance)
           {
                closest_target=target;
                distance=tem_dis;
           }
        }
        return closest_target;
    }
}

