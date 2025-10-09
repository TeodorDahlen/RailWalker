using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class HPbarUpdate : MonoBehaviour
{
    [SerializeField]
    private Slider HpBar;
    private Health health;

    private float maxHealth;
    private void Start()
    {
        health = GetComponent<Health>();
        health.OnDamaged += UpdateBar;
        maxHealth = health.GetHealth();

        UpdateBar(0f);
    }

    private void UpdateBar(float amount)
    {
        HpBar.value = health.GetHealth() / health.maxHealth;

        if (health.GetHealth() <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
    }
}
