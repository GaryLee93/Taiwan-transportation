using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_certain_destination : MonoBehaviour
{
    public IEnumerator move(Vector2 destination,int velocity,Rigidbody2D rb)
    {
        Vector2 v = destination-(Vector2)transform.position;
        Vector2 dir = v.normalized;
        rb.velocity=dir.normalized*velocity;
        yield return new WaitForSeconds(v.magnitude/velocity);
        rb.velocity=new Vector2(0f,0f);
    }
}
