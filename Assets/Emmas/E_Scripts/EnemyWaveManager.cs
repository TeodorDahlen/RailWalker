using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Collections;

//TODO: Add sound effects for wave start and wave cleared

//TODO: Update enemy count in EnemyWaveManager/EnemyCounter.cs when an enemy is spawned
//TODO: EnemyCounter script to call UpdateEnemyCount() on EnemyWaveManager when an enemy is destroyed

//TODO: Use naughty attributes to kill all enemies in scene for testing waves
//TODO: Add UI for wave number and enemies remaining (when we have UI)

//NOTE: Assign player manually in inspector
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
    private List<EnemyCounter> enemiesInScene;

    [Header("References")]

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private List<GameObject> enemySpawner;

    [SerializeField]
    private List<EnemyWave> enemySpawnerScript;
    private float timeBetweenWaves = 5f;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("Can't find player in scene, make sure player is assigned in inspector.");
            return;
        }

        enemySpawnerScript = new List<EnemyWave>();
        enemySpawnerScript.AddRange(GetComponentsInChildren<EnemyWave>());

        if (enemySpawnerScript.Count == 0)
        {
            Debug.LogError("No Enemy Wave scripts found on children of EnemyWaveManager object.");
            return;
        }

        enemySpawner = new List<GameObject>(GameObject.FindGameObjectsWithTag("EnemySpawner"));

        if (enemySpawner.Count == 0)
        {
            Debug.LogError("Can't find any enemy spawners in scene, make sure spawners have the 'EnemySpawner' tag assigned.");
            return;
        }

        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        waveEnemyTotalCount = CalculateEnemyWaveCount();
        enemiesRemaining = waveEnemyTotalCount;
        StartCoroutine(StartWave());

    }

    private int CalculateEnemyWaveCount()
    {
        return currentWave * waveMultiplier;
    }

    private IEnumerator StartWave()
    {
        Debug.Log("Starting Wave " + currentWave);

        yield return new WaitForSeconds(timeBetweenWaves);

        enemySpawnerScript.ForEach(spawner => spawner.SpawnEnemy());
    }

    public void UpdateEnemyCount()
    {
        enemiesRemaining--;
        Debug.Log(enemiesRemaining + " enemies remaining in wave " + currentWave);

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(WaveCleared());
        }
    }

    [Button("Force Next Wave")]
    private IEnumerator WaveCleared()
    {
        Debug.Log("Wave " + currentWave + " cleared!");
        currentWave++;
        waveEnemyTotalCount = CalculateEnemyWaveCount();
        enemiesRemaining = waveEnemyTotalCount;

        yield return new WaitForSeconds(timeBetweenWaves);
        
        StartCoroutine(StartWave());
    }

    void OnDisable()
    {
        currentWave = 1;
        waveEnemyTotalCount = 0;

        Debug.Log("Enemy Wave Manager reset to wave 1");
    }
}
