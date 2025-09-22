using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 20;

    private List<GameObject> poolOfObjects;

    void Start()
    {
        poolOfObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            poolOfObjects.Add(obj);
        }
    }

    public GameObject GetGameObject()
    {
        foreach (GameObject obj in poolOfObjects)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                Debug.Log("Taking object from pool");
                return obj;
            }
        }

        GameObject newObj = Instantiate(prefab);
        Debug.Log("Pool exhausted, instantiating new object");
        newObj.SetActive(true);
        poolOfObjects.Add(newObj);
        return newObj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        Debug.Log("Returning object to pool");
    }

    void OnDisable()
    {
        foreach (GameObject obj in poolOfObjects)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        poolOfObjects.Clear();
        Debug.Log("Object pool cleared");
    }
}
