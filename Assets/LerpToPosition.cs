using Unity.Mathematics;
using UnityEngine;

public class LerpToPosition : MonoBehaviour
{
    private Vector3 StartPos;
    private quaternion StartRotation;

    private void Start()
    {
        StartPos = transform.position;
        StartRotation = transform.rotation;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, StartPos, Time.deltaTime * 50);
        transform.rotation = Quaternion.Slerp(transform.rotation, StartRotation, Time.deltaTime * 50);
    }
}
