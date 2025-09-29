using UnityEngine;

//NOTE: Combining original BulletScript with ObjectPool - Marked up Teo's stuff
public class ReusableBullet : MonoBehaviour
{
    [Header("Bullet properties")]
    [SerializeField]
    private float Damage; //From Teo's BulletScript

    [SerializeField]
    private float maxSpeed = 20f; //From Teo's BulletScript

    [SerializeField]
    private float accelerationTime = 2f; //From Teo's BulletScript

    [SerializeField]
    public Vector3 direction; //From Teo's BulletScript

    [SerializeField]
    private float timeUntilDestruction = 1f;

    [SerializeField]
    private AudioClip shootingSound; //From Teo's BulletScript


    private float currentSpeed = 0f; //From Teo's BulletScript
    private float elapsedTime = 0f; //From Teo's BulletScript
    private ObjectPool objectPool;

    private AudioSource audioSource; //From Teo's BulletScript

    private float baseAudioStrenght; //From Teo's BulletScript


    private void Start()
    {
        objectPool = FindFirstObjectByType<ObjectPool>();

        //From Teo's BulletScript{
        direction = transform.forward;

        if (GetComponent<AudioSource>() != null)
        {
            audioSource = GetComponent<AudioSource>();
            baseAudioStrenght = audioSource.volume;
            PlayShooting();
        }
        //}
    }

    //From Teo's BulletScript{
    private void PlayShooting()
    {
        audioSource.pitch = Random.Range(0.95f, 1.05f);
        audioSource.volume = baseAudioStrenght * Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
    //}

    private void OnEnable()
    {
        elapsedTime = 0f;
        currentSpeed = 0f;
        //direction = transform.forward;
    }

    private void Update()
    {
        //From Teo's BulletScript{
        elapsedTime += Time.deltaTime;

        float t = Mathf.Clamp01(elapsedTime / accelerationTime);
        currentSpeed = maxSpeed * (1f - Mathf.Exp(-2f * t));

        transform.position += direction.normalized * currentSpeed * Time.deltaTime;
        //} 
    }

    void LateUpdate()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
        {
            return;
        }

        //From Teo's BulletScript 
        if (other.GetComponent<ExplodingCacti>() != null)
        {
            other.GetComponent<ExplodingCacti>().Explode();
        }

        objectPool.ReturnObject(gameObject);
        Debug.Log("Bullet hit: " + other.gameObject.name);
    }
}
