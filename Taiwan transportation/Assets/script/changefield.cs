using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class changefield : MonoBehaviour
{
    GameObject box;
       void Start()
    {
       pass.SetActive(false);
       bang.SetActive(false);
    }
    public GameObject uncle;
    public GameObject pass;
    public GameObject bang;

    void Update()
    {
        if(Input.GetKey("z"))
        {
            pass.SetActive(true);
            uncle.SetActive(false);
        }
        if(Input.GetKey("a"))
        {
            bang.SetActive(true);
            pass.SetActive(false);
        }
    }
}
