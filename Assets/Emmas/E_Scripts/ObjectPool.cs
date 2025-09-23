using System.Collections.Generic;
using UnityEngine;

//NOTE: Tags are important for this script to work properly
//NOTE: Bullet returns to pool on collision with anything except Player and Ground, and after a set time (found in gatling gun bullet script)
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
            GameObject obj = Instantiate(prefab, transform);
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

        GameObject newObj = Instantiate(prefab, transform);
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
