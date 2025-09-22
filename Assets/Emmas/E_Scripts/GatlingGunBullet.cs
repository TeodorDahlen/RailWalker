using UnityEngine;
using UnityEngine.Audio;

public class GatlingGunBullet : MonoBehaviour
{
    [SerializeField]
    private float Damage;

    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float accelerationTime = 2f;
    [SerializeField] public Vector3 direction;

    [SerializeField] private float timeUntilDestruction = 1f;

    private float currentSpeed = 0f;
    private float elapsedTime = 0f;
    private ObjectPool objectPool;

    private AudioSource audioSource;

    [SerializeField]
    private AudioClip shootingSound;
    private float baseAudioStrenght;

    private void Start()
    {
        objectPool = FindFirstObjectByType<ObjectPool>();

        direction = transform.forward;

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
            baseAudioStrenght = audioSource.volume;
            PlayShooting();
        }
    }

    private void OnEnable()
    {
        elapsedTime = 0f;
        currentSpeed = 0f;
        direction = transform.forward;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / accelerationTime);
        currentSpeed = maxSpeed * (1f - Mathf.Exp(-2f * t));

        transform.position += direction.normalized * currentSpeed * Time.deltaTime;


        BulletLifeTime();

    }

    public void SetPool(ObjectPool pool)
    {
        objectPool = pool;
    }


    private void BulletLifeTime()
    {
        if (elapsedTime >= timeUntilDestruction)
        {
            objectPool.ReturnObject(gameObject);
        }
    }

    private void PlayShooting()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.volume = baseAudioStrenght * Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ExplodingCacti>() != null)
        {
            other.GetComponent<ExplodingCacti>().Explode();
            objectPool.ReturnObject(gameObject);
        }
    }
}
