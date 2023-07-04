using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObj : MonoBehaviour
{
    public static StageObj instance;
    private void Awake() {
        instance = this;
    }

    [System.Serializable] public class NamePair{
        public string name;
        public GameObject gameobj;
    }
    [SerializeField] private NamePair[] enemy_arr;
    [SerializeField] private NamePair[] bullet_arr;
    [SerializeField] private NamePair[] collectable_arr;

    public static Dictionary<string, GameObject> Enemies;
    public static Dictionary<string, GameObject> Bullets;
    public static Dictionary<string, GameObject> Collectables;
    
    private void Start() {
        Enemies = new Dictionary<string, GameObject>();
        Bullets = new Dictionary<string, GameObject>();
        Collectables = new Dictionary<string, GameObject>();

        for(int i=0; i<enemy_arr.Length; i++){
            Enemies.Add(enemy_arr[i].name, enemy_arr[i].gameobj);
        }

        for(int i=0; i<bullet_arr.Length; i++){
            Bullets.Add(bullet_arr[i].name, bullet_arr[i].gameobj);
        }

        for(int i=0; i<collectable_arr.Length; i++){
            Collectables.Add(collectable_arr[i].name, collectable_arr[i].gameobj);
        }
    }

    /*
    How to use:
        example:
        StageObj.Enemies["scooter"]
        StageObj.Collectables["power"]
    */
}
