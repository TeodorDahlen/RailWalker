using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, moveSpeed) * Time.deltaTime;
    }
}
