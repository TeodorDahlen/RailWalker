using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentHealth;

    [Header("Death Effects")]
    [SerializeField]
    private GameObject DeathEffect;

    [SerializeField]
    private GameObject Core;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (GetComponent<OnHitEffect>() != null)
        {
            GetComponent<OnHitEffect>().SpawnHit();
        }
        if (currentHealth <= 0)
        {
            PlayDeath();
        }
    }

    public void PlayDeath()
    {
        GameObject newEffect = Instantiate(DeathEffect, transform.position, Quaternion.identity);
        Destroy(newEffect, 2);
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return currentHealth;   
    }


    public GameObject GetCore()
    {
        if (Core != null)
        {
            return Core;
        }
        else
        {
            return gameObject;
        }
    }
}
