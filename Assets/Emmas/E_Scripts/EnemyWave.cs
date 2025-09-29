using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [Header("Enemy Spawner Settings")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject enemyTarget;
    private float spawnAreaRange = 10;

    public void SpawnEnemy(EnemyWaveManager manager)
    {
        float randomX = Random.Range(-spawnAreaRange, spawnAreaRange);
        float randomZ = Random.Range(-spawnAreaRange, spawnAreaRange);

        Vector3 spawnPos = transform.position + new Vector3(randomX, 0f, randomZ);
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        newEnemy.GetComponent<BasicMonsterMovement>().Target = enemyTarget;

        EnemyCounter counter = newEnemy.GetComponent<EnemyCounter>();
        if (counter != null)
        {
            counter.Register(manager);
        }
        else
        {
            Debug.LogWarning("Spawned enemy missing EnemyCounter component: " + newEnemy.name);
        }
    }
}
