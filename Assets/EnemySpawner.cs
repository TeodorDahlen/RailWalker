using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyType1;
    [SerializeField]
    private GameObject Target;

    [SerializeField]
    private float SpawnRate;

    //[SerializeField]
    //private float spawnAmount;

    private float timer;

    [SerializeField]
    private float spawnAreaRange = 10;
    private void Start()
    {
        timer = SpawnRate;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            SpawnEnemy();
            timer = SpawnRate;
        }
    }

    private void SpawnEnemy()
    {
        float randomX = Random.Range(-spawnAreaRange, spawnAreaRange);
        float randomZ = Random.Range(-spawnAreaRange, spawnAreaRange);

        Vector3 spawnPos = transform.position + new Vector3(randomX, 0f, randomZ);
        GameObject newEnemy = Instantiate(enemyType1, spawnPos, Quaternion.identity);
        newEnemy.GetComponent<BasicMonsterMovement>().Target = Target;
    }
}
