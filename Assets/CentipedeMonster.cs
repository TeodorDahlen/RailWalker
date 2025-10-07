using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeMonster : MonoBehaviour
{
    [SerializeField]
    private int BodyAmount = 8;

    [SerializeField]
    private List<GameObject> Segments = new List<GameObject>();

    [SerializeField]
    private GameObject prefabSegment;


    //Movement
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float amplitude = 0.35f;
    [SerializeField] private float frequency = 2f;
    [SerializeField] private float phase = 0f;
    [SerializeField] public GameObject Target;
  
    private Vector3 direction;
    private Vector3 baseScale;

    private int DamageTaken = 0;
    private Health health;
    private void Start()
    {
        for (int i = 0; i < BodyAmount; i++)
        {
            GameObject newBody = Instantiate(prefabSegment, transform.position + new Vector3(0,0, 0.5f * i), Quaternion.identity);
            Segments.Add(newBody);
        }

        if (phase == 0f)
            phase = Random.Range(0f, Mathf.PI * 2f);

        Target = GetComponent<BasicMonsterMovement>().Target;

        health = GetComponent<Health>();
        health.SetHealth(BodyAmount * 10);
        health.OnDamaged += HandleDamage;
    }

    private void Update()
    {
        if (Target == null) return;

        Vector3 toTarget = Target.transform.position - transform.position;
        direction = toTarget.normalized;
        if (direction.sqrMagnitude > 0f)
            transform.forward = direction;

        float angle = Time.time * frequency + phase;
        float distance = toTarget.magnitude;
        float fade = Mathf.Clamp01(distance / 5f);

        // Side-to-side
        Vector3 side = Vector3.Cross(direction, Vector3.up).normalized * (amplitude * Mathf.Sin(angle) * fade);

        // Up-and-down
        Vector3 up = Vector3.up * (amplitude * Mathf.Cos(angle) * fade);

        // Combine offsets
        Vector3 swirlOffset = side + up;

        // Constant forward velocity
        Vector3 velocity = direction * movementSpeed + swirlOffset;

        transform.position += velocity * Time.deltaTime;

        UpdateBodies();
    }

    private void UpdateBodies()
    {
        for (int i = DamageTaken; i < Segments.Count; i++)
        {
            if (i - 1 >= DamageTaken)
            {
                Segments[i].transform.position = Vector3.Lerp(Segments[i].transform.position, Segments[i - 1].transform.position, Time.deltaTime * amplitude * 2);
            }
            else
            {
                Segments[i].transform.position = Vector3.Lerp(Segments[i].transform.position, transform.position, Time.deltaTime * amplitude * 2);
            }
        }
    }

    private void HandleDamage(float damage)
    {
        transform.position = Segments[DamageTaken].transform.position;
        Segments[DamageTaken].SetActive(false);
        DamageTaken++;
    }

}
