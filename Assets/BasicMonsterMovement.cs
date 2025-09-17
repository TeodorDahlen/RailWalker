using UnityEngine;

public class BasicMonsterMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed = 10;

    [SerializeField]
    public GameObject Target;

    private Vector3 direction;


    private void Update()
    {
        direction = (Target.transform.position - transform.position).normalized;
        transform.forward = direction;
        //direction = new Vector3(direction.x, 0, direction.z);

        transform.position += direction * Time.deltaTime * movementSpeed;
    }
}
