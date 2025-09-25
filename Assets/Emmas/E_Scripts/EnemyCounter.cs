using UnityEngine;

//TODO: Call UpdateEnemyCount() on EnemyWaveManager when an enemy is destroyed
//TODO: Update enemy count in EnemyWaveManager when an enemy is spawned
public class EnemyCounter : MonoBehaviour
{
    [SerializeField]
    private EnemyWaveManager enemyWaveManager;

    private void Start()
    {
        enemyWaveManager = FindFirstObjectByType<EnemyWaveManager>();

        if (enemyWaveManager == null)
        {
            Debug.LogError("EnemyWaveManager reference not set in EnemyCounter script on " + gameObject.name);
        }
    }

    public GameObject CalculateEnemyCount()
    {
        return gameObject;
    }

    private void OnDestroy()
    {
        if (enemyWaveManager != null)
        {
            enemyWaveManager.UpdateEnemyCount();
        }
    }
}
