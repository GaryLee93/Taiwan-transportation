using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_recycle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.tag=="border"||other.gameObject.tag=="enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
