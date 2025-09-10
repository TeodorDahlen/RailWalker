using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth;

    private float currentHealth;

    [Header("Death Effects")]
    [SerializeField]
    private GameObject DeathEffect;


    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
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
}
