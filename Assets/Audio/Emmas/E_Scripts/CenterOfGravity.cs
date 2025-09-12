using System;
using UnityEngine;

public class CenterOfGravity : MonoBehaviour
{
    public Rigidbody rb;
    public Transform centerOfGravityPoint;

    void Start()
    {
        if (rb == null)
        {
            rb = GetComponentInParent<Rigidbody>();
            Debug.LogWarning("Rigidbody not assigned. Attempting to find Rigidbody in parent.");
        }

        if (centerOfGravityPoint != null)
        {
            SetCOG();
        }
    }

    private void SetCOG()
    {
        rb.centerOfMass = centerOfGravityPoint.localPosition;
    }

    void Update()
    {
        if (rb != null && centerOfGravityPoint != null)
        {
            SetCOG();
        }
    }

}
