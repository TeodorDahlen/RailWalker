using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;
using System.Collections;

public class EnemyWaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int currentWave = 1;
    [SerializeField] private int waveMultiplier = 5;
    [SerializeField] private float baseTimeBetweenWaves = 5f;
    [SerializeField] private float timeMultiplyer = 1.2f;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private List<GameObject> enemySpawner;
    [SerializeField] private List<EnemyWave> enemySpawnerScript;
    [SerializeField] private WaveUIManager waveUIManager;

    private float timeBetweenWaves;
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

    public int GetCurrentWave(int currentwave)
    {
        return currentWave;        
    }
    
    public float GetTimeBetweenWaves(float timeBetweenWaves)
    {
        return timeBetweenWaves;
    }
    private int CalculateEnemyWaveCount()
    {
        return currentWave * waveMultiplier;
    }

    private IEnumerator StartWave()
    {
        if (waveUIManager != null)
        {
            waveUIManager.ShowWaveStartText(currentWave);
        }

        else
        {
            Debug.LogWarning("WaveUIManager reference is not assigned in the inspector.");
        }

        yield return new WaitForSeconds(2f);

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

        if (enemiesRemaining <= 0)
        {
            StartCoroutine(WaveCleared());
        }
    }

    [Button("Force Next Wave")]
    private IEnumerator WaveCleared()
    {

        currentWave++;

        if (currentWave % 5 == 0)
        {
            timeBetweenWaves = baseTimeBetweenWaves * timeMultiplyer;
        }

        else
        {
            timeBetweenWaves = baseTimeBetweenWaves;
        }

        if (waveUIManager != null)
        {
            waveUIManager.StartCountdown(timeBetweenWaves);
        }
        else
        {
            Debug.LogWarning("WaveUIManager reference is not assigned in the inspector.");
        }

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
