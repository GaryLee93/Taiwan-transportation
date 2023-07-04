using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObj : MonoBehaviour
{
    public static StageObj instance;

    private void Awake() {
        instance = this;
    }

    GameObject[] Enemies;
    GameObject[] Bullets;
    
    Dictionary<string, GameObject> Collectables;

}
