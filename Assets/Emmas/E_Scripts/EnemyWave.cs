using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
    [Header("Enemy Spawner Settings")]
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private List<GameObject> EnemyTypes = new List<GameObject>();
    [SerializeField]
    private GameObject enemyTarget;
    private float spawnAreaRange = 10;

    public void SpawnEnemy(EnemyWaveManager manager, int Tokens)
    {
        while (Tokens > 0)
        {
            float randomX = Random.Range(-spawnAreaRange, spawnAreaRange);
            float randomZ = Random.Range(-spawnAreaRange, spawnAreaRange);

            Vector3 spawnPos = transform.position + new Vector3(randomX, 0f, randomZ);


            int randomEnemy = Random.Range(1, EnemyTypes.Count + 1);
            int cost = int.MaxValue;
            GameObject prefabToSpawn = null; 
            switch (randomEnemy)
            {
                case 1:
                    prefabToSpawn = EnemyTypes[0];
                    cost = 1;
                    break;

                case 2:
                    prefabToSpawn = EnemyTypes[1];
                    cost = 5;
                    break;

                case 3:
                    prefabToSpawn = EnemyTypes[2];
                    cost = 20;
                    break;
            }

            // If we can't afford this one, loop again
            if (Tokens < cost)
            {
                if(Tokens > 5)
                {
                    prefabToSpawn = EnemyTypes[1];
                    cost = 5;
                }
                else
                {
                    prefabToSpawn = EnemyTypes[0];
                    cost = 1;
                }
            }

            GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, Quaternion.identity);
            Tokens -= cost;
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
}
