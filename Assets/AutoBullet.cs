using UnityEngine;

public class AutoBullet : MonoBehaviour
{
    [SerializeField]
    public GameObject target;

    private Vector3 direction;

    [SerializeField]
    private float targetingStrength = 2;
    private void Update()
    {
        if(target != null)
        {
            direction = (target.transform.position - transform.position).normalized;
            transform.position += direction * Time.deltaTime * targetingStrength;
        }
    }
}
