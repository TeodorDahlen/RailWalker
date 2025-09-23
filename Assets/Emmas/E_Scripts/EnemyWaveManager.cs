using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

//TODO: Use naughty attributes to kill all enemies in scene for testing waves
public class EnemyWaveManager : MonoBehaviour
{
    [Header("Wave Settings")]

    [SerializeField]
    private int currentWave = 1;

    [SerializeField]
    private int waveMultiplier = 5;

    [SerializeField]
    private int waveEnemyTotalCount;

    [SerializeField]
    private int enemiesRemaining;

    [SerializeField]
    private List<GameObject> enemySpawner;

    private GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Can't find player in scene, make sure player has the 'Player' tag assigned.");
            return;
        }

        enemySpawner = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawner"));
        
        if (enemySpawner.Count == 0)
        {
            Debug.LogError("Can't find any enemy spawners in scene, make sure spawners have the 'EnemySpawner' tag assigned.");
            return;
        }

        else
            Debug.Log("Found " + enemySpawner.Count + " enemy spawners in the scene.");

    }

    void Update()
    {

    }
}
