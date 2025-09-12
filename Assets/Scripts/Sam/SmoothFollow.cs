using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    public Transform target;
    public Transform playerCamera;
    public float smoothTime = 0.3f;
    public Vector3 velocity = Vector3.zero;
    public float rotationSpeed = 5f;

    private void Update()
    {
        //get to player
        transform.position = Vector3.SmoothDamp(transform.position, target.position, ref velocity, smoothTime);

        // vector point from text to player
        Vector3 directionToCamera = playerCamera.position - transform.position;
        //ignores vertical diffrence so it wont tilt etc
        directionToCamera.y = 0; 

        //lengjt of the vector square. If the text is same pos as player  the vector should be 0 
        if (directionToCamera.sqrMagnitude > 0.001f) 
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToCamera);

            // it was mirrored before so fixing that 
            targetRotation *= Quaternion.Euler(0, 180f, 0);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

    }
}
