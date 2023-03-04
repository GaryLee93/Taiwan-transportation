using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courtine_test : MonoBehaviour
{
    public List<int> num= new List<int> {1,2,3,4,5};
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(printNum(num));
        }
    }

    IEnumerator printNum(List<int> num)
    {
        foreach(int i in num)
        {
            Debug.Log(i);
            yield return null;
        }
    }
}
