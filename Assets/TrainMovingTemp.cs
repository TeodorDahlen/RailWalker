using UnityEngine;

public class TrainMovingTemp : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,0,1) * Time.deltaTime * moveSpeed;
    }
}
