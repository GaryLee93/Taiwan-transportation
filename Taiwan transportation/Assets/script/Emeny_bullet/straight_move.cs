using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class straight_move : MonoBehaviour
{
    private void Start() 
    {
        gameObject.transform.parent = GameObject.Find("texi").transform;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = 3*transform.localPosition.normalized;
        gameObject.transform.parent = null;
    }
}
