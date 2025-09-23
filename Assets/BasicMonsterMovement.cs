using UnityEngine;

public class BasicMonsterMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float amplitude = 0.35f;
    [SerializeField] private float frequency = 2f;
    [SerializeField] private float phase = 0f;
    [SerializeField] public GameObject Target;

    private Vector3 direction;

    [SerializeField]
    private GameObject EnemySail;

    private Vector3 baseScale;

    private AudioSource audioSource;
    private bool hasPlayedAudio;
    private float baseVolume;

    [SerializeField]
    private GameObject attackEffect;
    private void Start()
    {
        if (phase == 0f)
            phase = Random.Range(0f, Mathf.PI * 2f);

        baseScale = EnemySail.transform.localScale;
        audioSource = GetComponent<AudioSource>();
        baseVolume = audioSource.volume;
    }

    private void Update()
    {
        if (Target == null) return;

        Vector3 toTarget = Target.transform.position - transform.position;
        direction = new Vector3(toTarget.x, toTarget.y, toTarget.z).normalized;
        if (direction.sqrMagnitude > 0f)
            transform.forward = direction;

        float sin = Mathf.Sin(Time.time * frequency + phase);
        float speedMultiplier = 1f + amplitude * sin;
        float currentSpeed = Mathf.Max(0f, movementSpeed * speedMultiplier);

        transform.position += direction * currentSpeed * Time.deltaTime;

        if (sin > 0)
            EnemySail.transform.localScale = baseScale * (1 - sin * 0.25f);
        else
            EnemySail.transform.localScale = baseScale;

        if(sin >= -0.2f && hasPlayedAudio == false)
        {
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.volume = baseVolume * Random.Range(0.9f, 1.1f);
            audioSource.Play();
            if(attackEffect != null)
            {
                GameObject newVFX = Instantiate(attackEffect, transform.position, Quaternion.identity);
                Destroy(newVFX, 0.5f);
            }
            hasPlayedAudio = true;
        }

        if(sin < -0.25f)
        {
            hasPlayedAudio = false;
        }
    }
}
