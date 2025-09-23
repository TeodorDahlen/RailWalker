using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GeneratingTerrain : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> TerrainPices = new List<GameObject>();

    private void Update()
    {
        foreach (GameObject terrain in TerrainPices)
        {
            if(terrain.transform.position.z <= -300)
            {
                terrain.transform.position = new Vector3(terrain.transform.position.x, terrain.transform.position.y, 300);
            }
        }
    }
}
