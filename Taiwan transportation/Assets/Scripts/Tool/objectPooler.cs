using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectPooler : MonoBehaviour
{
    [System.Serializable]
    public class pool
    {
        public string name;
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
    Dictionary<GameObject, string> objNameDict;
    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        objNameDict = new Dictionary<GameObject, string>();
        foreach(pool p in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i=0;i<p.size;i++)
            {
                GameObject obj = Instantiate(p.perfab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(p.name,objectPool);
            objNameDict.Add(p.perfab, p.name);
        }
    }

    public static GameObject spawnFromPool(string tag,Vector2 position,Quaternion rotation)
    {
        if(!instance.poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("沒有 tag="+tag+" 的物件池ㄛ");
            return null;
        }
        GameObject objectToSpawn = instance.poolDictionary[tag].Dequeue();
        if(objectToSpawn.activeSelf == true){
            Debug.LogWarning("物件池 " +tag +" 數量不足ㄛ");
        }
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position=position;
        objectToSpawn.transform.rotation=rotation;

        Ipooled ip = objectToSpawn.GetComponent<Ipooled>();
        if(ip!=null)
        {
            ip.onBulletSpawn();
        }

        instance.poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    public static GameObject spawnFromPool(GameObject objToSpawn,Vector2 position,Quaternion rotation)
    {   
        if(!instance.objNameDict.ContainsKey(objToSpawn)){
            Debug.LogWarning("物件" + objToSpawn.name + "不在物件池ㄛ");
            return null;
        }
        else{
            return spawnFromPool(instance.objNameDict[objToSpawn] , position, rotation);
        }
    }
}
