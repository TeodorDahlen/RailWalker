using UnityEngine;

public class ParticleSound : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayDeath();
    }
    private void PlayDeath()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.volume = audioSource.volume * Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}
