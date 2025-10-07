using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;

public class SpakFollowRotation : MonoBehaviour
{
    [SerializeField] Transform followCube;
    [SerializeField] bool followRotation = false;

    //när spelaren drag cuben på z axis så ska spaken "dras" på rotation x axis

    private float rotationspeed = 10f;

    float cubeZaxis = 0f;
    float rotationAaxis = 0f;

    void Update()
    {
        if (followRotation)
        {
            SpakFollowRotationGrab();
        }
    }

    private void SpakFollowRotationGrab()
    {
        Debug.Log("spakfollowrotation here");

        Vector3 targetPosition = new Vector3(this.transform.position.x,
                                       followCube.position.y,
                                       followCube.position.z);
        this.transform.LookAt(targetPosition);


        //cubeZaxis = followCube.rotation.z;
        //float deltaZ = cubeZaxis - startZ;
        //rotationAaxis = cubeZaxis * rotationspeed;

        //transform.rotation = quaternion.Euler(rotationAaxis, 0f, 0f);

        //Debug.Log($"transform rotation of spak is {transform.rotation}]");
    }

    [Button]
    public void clickMe()
    {
        Debug.Log($"cube z axis is: {cubeZaxis}");
        Debug.Log($"rotationAxis is: {rotationAaxis}");
        Debug.Log($"transform rotation of spak is {transform.rotation}");
    }

    public void followRotationTrue()
    {
        followRotation = true;
        Debug.Log("bool true");
    }

    public void followRotationFalse()
    {
        followRotation = false;
        Debug.Log("bool false");
    }
}
