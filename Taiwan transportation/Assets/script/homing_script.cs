using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class homing_script  : MonoBehaviour
{
    public Transform target;
    public float speed=5f;
    private Rigidbody2D rb;
    public float rotateSpeed=200f;
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() 
    {
        Vector2 direction=(Vector2)target.position-rb.position;
        direction.Normalize();
        float rotateAmount=Vector3.Cross(direction,transform.up).z;
        rb.angularVelocity = -rotateAmount*rotateSpeed;
        rb.velocity=transform.up*speed;
    }
}
