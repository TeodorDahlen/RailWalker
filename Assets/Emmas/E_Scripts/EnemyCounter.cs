using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    private EnemyWaveManager enemyWaveManager;

    public void Register(EnemyWaveManager manager)
    {
        enemyWaveManager = manager;

        if (enemyWaveManager != null)
        {
            enemyWaveManager.EnemySpawned();
        }
        else
        {
            Debug.LogError("EnemyWaveManager reference missing when registering enemy: " + gameObject.name);
        }
    }

    private void OnDestroy()
    {
        if (enemyWaveManager != null)
        {
            enemyWaveManager.UpdateEnemyCount();
        }
    }
}
