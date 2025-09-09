using UnityEngine;
using NaughtyAttributes;
using System;

public class GatlingGun : MonoBehaviour
{
    [Header("Barrel Object")]
    [SerializeField] private Transform barrelTransform;

    [Header("Gatling Gun Settings")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float radius = 0.5f;   // distance from center
    [SerializeField] private float spinSpeed = 360f; // degrees per second

    private GunBase gunBase;
    private GameObject shootingPoint;
    private Vector3 originalLocalPos;
    private float angle;

    private void Awake()
    {
        gunBase = GetComponent<GunBase>();

        if (gunBase != null)
        {
            // Access the private serialized ShootingPoint via reflection
            var field = typeof(GunBase).GetField("ShootingPoint", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            shootingPoint = field?.GetValue(gunBase) as GameObject;

            if (shootingPoint != null)
                originalLocalPos = shootingPoint.transform.localPosition;
        }
    }

    [Button]
    public void ConstantFire()
    {
        // Start invoking the firing method at the specified fire rate
        if (gunBase != null && shootingPoint != null)
        {
            InvokeRepeating(nameof(FireFromRotatingPoint), 0f, fireRate);
            InvokeRepeating(nameof(RotateBarrel), 0f, fireRate); // Rotate barrel every 0.02 seconds
        }
        else
            Debug.LogWarning("GunBase or ShootingPoint not found on GatlingGun object.");
    }

    private void RotateBarrel()
    {
        if (barrelTransform == null)
            return;

        // Rotate the barrel around the Z-axis
        barrelTransform.Rotate(new Vector3(0, 0, 1), spinSpeed * fireRate, Space.Self);
    }

    private void FireFromRotatingPoint()
    {
        if (shootingPoint == null) return;

        // Updated angle based on spin speed and fire rate
        angle += spinSpeed * fireRate;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f) * radius;

        // Applying offset to shooting point
        shootingPoint.transform.localPosition = originalLocalPos + offset;

        gunBase.Shoot();
    }

    [Button]
    public void StopFiring()
    {
        CancelInvoke(nameof(FireFromRotatingPoint));
        CancelInvoke(nameof(RotateBarrel));

        // reset shooting point position to original firePoint
        if (shootingPoint != null)
            shootingPoint.transform.localPosition = originalLocalPos;
    }
}
