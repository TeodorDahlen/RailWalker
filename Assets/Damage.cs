using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField]
    public float damage;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
