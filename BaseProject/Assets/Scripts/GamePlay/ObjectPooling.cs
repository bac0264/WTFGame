using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    //public static ObjectPooling instance;
    public List<GameObject> pooledObjects = new List<GameObject>();
    public GameObject prefFabs;
    public Transform ObjectPoolingManager;
    public int amountToPool;
    private void OnDisable()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            pooledObjects[i].SetActive(false);
        }
    }
    private void Awake()
    {
        //if (instance == null) instance = this;
    }
    private void OnValidate()
    {
        if (pooledObjects.Count == 0)
        {
            for (int i = 0; i < amountToPool; i++)
            {
                GameObject obj = Instantiate(prefFabs, ObjectPoolingManager);
                obj.SetActive(false);
                pooledObjects.Add(obj);
            }
        }
    }
    public GameObject getObjectPooling()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                pooledObjects[i].SetActive(true);
                return pooledObjects[i];
            }
        }
        return null;
    }

}
