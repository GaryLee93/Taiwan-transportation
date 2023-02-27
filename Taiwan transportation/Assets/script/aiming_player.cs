using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aiming_player : MonoBehaviour
{
    public float speed;
    GameObject target;
    Rigidbody2D rb;
    void Start()
    {
        target=GameObject.FindGameObjectWithTag("Player");
        rb=GetComponent<Rigidbody2D>();
        Vector2 direction=(target.transform.position-transform.position);
        direction.Normalize();
        rb.velocity=direction*speed;
    }
}
