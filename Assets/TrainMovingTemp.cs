using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class TrainMovingTemp : MonoBehaviour
{
    public static TrainMovingTemp Instance;

    [SerializeField] private float moveSpeed = 2f;
    public List<GameObject> everything = new List<GameObject>();

    
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // Find all objects with WorldObject script and store their GameObjects
        WorldObject[] newArray = FindObjectsOfType<WorldObject>();
        foreach (WorldObject obj in newArray)
        {
            everything.Add(obj.gameObject);
        }

        
    }

    private void Update()
    {
        // Move all objects forward at the same speed
        foreach (GameObject go in everything)
        {
            go.transform.position -= Vector3.forward * moveSpeed * Time.deltaTime; //ChatGPT
        }
    }
}
