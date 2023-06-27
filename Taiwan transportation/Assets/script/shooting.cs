using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class shooting : MonoBehaviour
{
    public GameObject bullet_1;
    public GameObject bullet_2;
    orbit_half_cycle ro;
    IEnumerator rice_sea()
    {
        yield return new WaitForSeconds(0.5f);
    }
     
}
