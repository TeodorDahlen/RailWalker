using UnityEngine;

public class OnHitEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject HitEffect;

    public void SpawnHit()
    {
        GameObject newEffect = Instantiate(HitEffect, transform.position + Random.insideUnitSphere * 0.5f, Quaternion.identity);
        Destroy(newEffect, 2);
    }
}
