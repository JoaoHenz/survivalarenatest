using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ObjectToPool
{
    public string key;
    public GameObject gameObject;
    public int amount;

    public ObjectToPool(GameObject gameObject, int amount)
    {
        this.gameObject = gameObject;
        this.amount = amount;
    } 
}

public class ObjectPool
{
    public List<GameObject> gameObjects;
    public int index;

    public ObjectPool(List<GameObject> gameObjects)
    {
        this.gameObjects = gameObjects;
        this.index = 0;
    }
}


public class PoolManager : MonoBehaviour {
    public static  PoolManager instance;

    static Dictionary<string, ObjectPool> objectPools;
    public List<ObjectToPool> objectsToPool;


    void Start () {
        instance = this;
        objectPools = new Dictionary<string, ObjectPool>();
        foreach (ObjectToPool obj in objectsToPool)
        {
            List<GameObject> listObj = new List<GameObject>();
            for (int i =0; i < obj.amount; i++)
            {
                GameObject toBePooled = Instantiate(obj.gameObject);
                toBePooled.SetActive(false);
                toBePooled.transform.parent = this.transform;
                listObj.Add(toBePooled);
            }
            ObjectPool objectPool = new ObjectPool(listObj);
            objectPools.Add(obj.key, objectPool);
        }

	}
	
	public static GameObject GetObject(string key)
    {
        if (!objectPools.ContainsKey(key))
            return null;
        else
        {
            ObjectPool objectPool = objectPools[key];
            if(objectPool.index >= objectPool.gameObjects.Count - 1) //chegou no último membro
            {
                objectPool.index = 0;
            }
            else
            {
                objectPool.index++;
            }

            return objectPool.gameObjects[objectPool.index];

        }
    }
}
