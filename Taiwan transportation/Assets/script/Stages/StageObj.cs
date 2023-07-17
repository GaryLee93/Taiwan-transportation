using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageObj : MonoBehaviour
{
    public static StageObj instance;

    [System.Serializable] public class NamePair{
        public string name;
        public GameObject gameobj;
    }
    [SerializeField] private NamePair[] enemy_arr;
    [SerializeField] private NamePair[] bullet_arr;
    [SerializeField] private NamePair[] collectable_arr;
    [SerializeField] private NamePair[] text_arr;

    public static Dictionary<string, GameObject> Enemies;
    public static Dictionary<string, GameObject> Bullets;
    public static Dictionary<string, GameObject> Collectables;
    public static Dictionary<string, GameObject> StageTexts;
    
    private void Awake() {
        instance = this;
        
        Enemies = new Dictionary<string, GameObject>();
        Bullets = new Dictionary<string, GameObject>();
        Collectables = new Dictionary<string, GameObject>();
        StageTexts = new Dictionary<string, GameObject>();

        for(int i=0; i<enemy_arr.Length; i++){
            Enemies.Add(enemy_arr[i].name, enemy_arr[i].gameobj);
        }

        for(int i=0; i<bullet_arr.Length; i++){
            Bullets.Add(bullet_arr[i].name, bullet_arr[i].gameobj);
        }

        for(int i=0; i<collectable_arr.Length; i++){
            Collectables.Add(collectable_arr[i].name, collectable_arr[i].gameobj);
        }

        for(int i=0; i<text_arr.Length; i++){
            StageTexts.Add(text_arr[i].name, text_arr[i].gameobj);
        }
    }
    public static void eraseAllBullet()
    {
        GameObject[] bullets = GameObject.FindGameObjectsWithTag("enemy_bullet");
        foreach(var bullet in bullets)
        {
            bullet.GetComponent<EnemyBullet>().poolDespawn();
        }
    }
    /*
    How to use:
        example:
        StageObj.Enemies["scooter"]
        StageObj.Collectables["power"]
    */
}
