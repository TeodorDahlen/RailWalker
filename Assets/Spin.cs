using UnityEngine;

public class Spin : MonoBehaviour
{

    [SerializeField]
    private Vector3 rotationSpeed;
    private void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
