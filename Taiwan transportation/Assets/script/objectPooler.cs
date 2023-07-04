using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    [System.Serializable]
    public class pool
    {
        public string tag;
        public GameObject perfab;
        public int size;
    }
    
    #region Singleton
    public static objectPooler instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public List<pool> pools;
    public Dictionary<string,Queue<GameObject>> poolDictionary;
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(pool p in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i=0;i<p.size;i++)
            {
                GameObject obj = Instantiate(p.perfab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(p.tag,objectPool);
        }
    }

    public GameObject spawnFromPool(string tag,Vector2 position,Quaternion rotation,GameObject newParent)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with  tag "+tag+" doesn't exist");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position=position;
        objectToSpawn.transform.rotation=rotation;

        Ipooled ip = objectToSpawn.GetComponent<Ipooled>();
        if(ip!=null)
        {
            ip.setParent(newParent);
            ip.onBulletSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
}
