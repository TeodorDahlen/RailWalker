using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Collections;

public class EnemyWaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int currentWave = 1;
    [SerializeField] private int waveMultiplier = 5;
    [SerializeField] private float timeBetweenWaves = 5f;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemySpawner;
    [SerializeField] private List<EnemyWave> enemySpawnerScript;

    private int waveEnemyTotalCount;
    private int enemiesRemaining;

    void Start()
    {
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
        enemiesRemaining = 0;
        StartCoroutine(StartWave());
    }

    private int CalculateEnemyWaveCount()
    {
        return currentWave * waveMultiplier;
    }

    private IEnumerator StartWave()
    {
        Debug.Log(waveEnemyTotalCount + " enemies spawning in wave " + currentWave);

        yield return new WaitForSeconds(timeBetweenWaves);

        int enemiesToSpawn = waveEnemyTotalCount;

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int spawnerIndex = Random.Range(0, enemySpawnerScript.Count);
            enemySpawnerScript[spawnerIndex].SpawnEnemy(this);
        }
    }

    public void EnemySpawned()
    {
        enemiesRemaining++;
    }

    public void UpdateEnemyCount()
    {
        enemiesRemaining--;
        Debug.Log("Enemy dead. Total enemies remaining: " + enemiesRemaining);

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(WaveCleared());
        }
    }

    [Button("Force Next Wave")]
    private IEnumerator WaveCleared()
    {
        Debug.Log("Wave " + currentWave + " cleared! Preparing for next wave...");
        currentWave++;
        waveEnemyTotalCount = CalculateEnemyWaveCount();
        enemiesRemaining = 0;

        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(StartWave());
    }

    void OnDisable()
    {
        currentWave = 1;
        waveEnemyTotalCount = 0;
        enemiesRemaining = 0;

        Debug.Log("Enemy Wave Manager reset to wave 1");
    }
}
