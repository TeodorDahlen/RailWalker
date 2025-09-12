using UnityEngine;
using NaughtyAttributes;

public class GatlingGun : MonoBehaviour
{
    [Header("Barrel Object")]
    [SerializeField] private Transform barrelTransform;

    [Header("Gatling Gun Settings")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float spinSpeed = 360f;
    [SerializeField] private Animation recoil;

    [SerializeField]
    private GameObject Projectile;

    [SerializeField]
    private GameObject ShootingVFX;

    [SerializeField]
    private GameObject shootingPoint;


    public float radius = 0.5f;
    public float maxDistance = 1000f;
    public LayerMask hitLayers;

    private Vector3 originalLocalPos;


    //Start firing the gun at a set rate
    [Button]
    public void ConstantFire()
    {
        if (shootingPoint != null)
        {
            // Start invoking the firing method at the specified fire rate
            InvokeRepeating(nameof(FireFromRotatingPoint), 0f, fireRate);
            InvokeRepeating(nameof(RotateBarrel), 0f, fireRate);
        }
        else
            Debug.LogWarning("GunBase or ShootingPoint not found on GatlingGun object.");
    }

    //Rotate the barrel object as gun fires
    private void RotateBarrel()
    {
        if (barrelTransform == null)
            return;

        barrelTransform.Rotate(new Vector3(0, 0, 1), spinSpeed * fireRate, Space.Self);
    }

    //Fire from set point as barrel rotates
    private void FireFromRotatingPoint()
    {
        if (shootingPoint == null) return;

        Shoot();
        if (recoil != null)
            recoil.Play();
    }

    [Button]
    //Stop firing the gun (Debugging purposes)
    public void StopFiring()
    {
        CancelInvoke(nameof(FireFromRotatingPoint));
        CancelInvoke(nameof(RotateBarrel));

        if (shootingPoint != null)
            shootingPoint.transform.localPosition = originalLocalPos;
    }

    /// <summary>
    /// Teos asabra gun base shoot method
    /// </summary>
    public void Shoot()
    {
        RaycastHit hit;
        Vector3 origin = shootingPoint.transform.position;
        Vector3 direction = shootingPoint.transform.forward;

        //Debug.DrawLine(origin, direction * maxDistance, Color.red, 2);

        if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance, hitLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        shootingPoint.transform.rotation = transform.rotation;
        GameObject bullet = Instantiate(Projectile, shootingPoint.transform.position, shootingPoint.transform.rotation);
        GameObject newVFX = Instantiate(ShootingVFX, shootingPoint.transform.position, Quaternion.LookRotation(GetBulletDirection(shootingPoint.transform, spreadAngle)));
        Destroy(newVFX, 0.5f);

        /* New Bullet Spread Start */
        if (useRandomSpread)
        {
            bullet.transform.rotation = Quaternion.LookRotation(GetBulletDirection(shootingPoint.transform, spreadAngle));
            newVFX.transform.rotation = Quaternion.LookRotation(GetBulletDirection(shootingPoint.transform, spreadAngle));
        }
        /* New Bullet Spread End */
    }

    /* Bullet Spread */
    public bool useRandomSpread = false;
    [SerializeField, Range(0, 90)] private float spreadAngle = 90f;

    public static Vector3 GetBulletDirection(Transform origin, float spread)
    {
        Vector3 dir = origin.forward;

        // Rotate randomly on X or Y by ±spread
        if (UnityEngine.Random.value < 0.5f)
            dir = Quaternion.AngleAxis(UnityEngine.Random.Range(-spread, spread), origin.right) * dir;
        else
            dir = Quaternion.AngleAxis(UnityEngine.Random.Range(-spread, spread), origin.up) * dir;

        Vector3 finalDir = dir.normalized;

        Debug.DrawRay(origin.position, finalDir * 3f, Color.red, 2f);

        return finalDir;
    }
}








