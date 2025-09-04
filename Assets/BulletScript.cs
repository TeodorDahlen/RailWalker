using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float Speed;

    [SerializeField]
    private float Damage;

    private void Start()
    {
        Destroy(gameObject, 5);
    }
    private void Update()
    {
        transform.position += transform.forward * Time.deltaTime * Speed;
    }
}
