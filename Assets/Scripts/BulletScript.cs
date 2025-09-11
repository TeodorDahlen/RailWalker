using UnityEngine;
using UnityEngine.Audio;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float Damage;

    [SerializeField] private float maxSpeed = 20f;        
    [SerializeField] private float accelerationTime = 2f;
    [SerializeField] public Vector3 direction;

    private float currentSpeed = 0f;
    private float elapsedTime = 0f;

    private AudioSource audioSource;
    [SerializeField]
    private AudioClip shootingSound;
    private float baseAudioStrenght;

    private void Start()
    {
        Destroy(gameObject, 5);
        direction = transform.forward;
        
        audioSource = GetComponent<AudioSource>();
        baseAudioStrenght = audioSource.volume;
        PlayShooting();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
    
        // Exponential-like increase
        // At t=0 -> 0, at t=accelerationTime -> ~0.95 maxSpeed
        float t = Mathf.Clamp01(elapsedTime / accelerationTime);
        currentSpeed = maxSpeed * (1f - Mathf.Exp(-2f * t));

        transform.position += direction.normalized * currentSpeed * Time.deltaTime;
    }

    private void PlayShooting()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.volume = baseAudioStrenght * Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<ExplodingCacti>() != null)
        {
            other.GetComponent<ExplodingCacti>().Explode();
        }
    }
}
