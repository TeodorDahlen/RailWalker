using UnityEngine;
using NaughtyAttributes;
using static UnityEngine.InputSystem.InputAction;

public class GatlingGun : MonoBehaviour
{
    [Header("Barrel Object")]
    [SerializeField] private Transform barrelTransform;

    [Header("Gatling Gun Settings")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float spinSpeed = 360f;
    [SerializeField] private Animation recoil;

    [Header("Bullet Settings")]
    [SerializeField] private ObjectPool bulletPool;
    [SerializeField] private GameObject ShootingVFX;
    [SerializeField] private GameObject shootingPoint;
    [SerializeField] private AmmoManager ammoManager;
    public bool canFire = true;

    [Header("Raycast Settings")]
    public float radius = 0.5f;
    public float maxDistance = 1000f;
    public LayerMask hitLayers;

    private Vector3 originalLocalPos;


    private bool CanShoot()
    {
        ammoManager.HasAmmo();
        return ammoManager.HasAmmo() && canFire;
    }

    //Start firing the gun at a set rate
    [Button]
    public void ConstantFire()
    {
        canFire = CanShoot();

        if (!canFire)
        {
            Debug.Log("Out of Ammo, need coal");
            StopFiring();
        }

        else
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
        //Bool added to check if we can shoot based on heat in furnace
        canFire = CanShoot();

        if (canFire == false)
        {
            Debug.Log("StopFiring() triggered in Shoot(), need more coal");
            StopFiring();
            return;
        }

        else
        {
            float amount = ammoManager.amountToDeplate;
            ammoManager.DeplateAmmo(amount);

            RaycastHit hit;
            Vector3 origin = shootingPoint.transform.position;
            Vector3 direction = shootingPoint.transform.forward;

            // In GatlingGun.Shoot
            GameObject bullet = bulletPool.GetGameObject();
            bullet.transform.position = shootingPoint.transform.position;
            bullet.transform.rotation = shootingPoint.transform.rotation;

            // Tell the bullet which pool it belongs to
            bullet.GetComponent<ReusableBullet>().SetPool(bulletPool);


            if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance, hitLayers))
            {
                //Debug.Log("Hit: " + hit.collider.name);
            }

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

    public void TriggerPressed(CallbackContext context)
    {
        if (context.started)
        {
            ConstantFire();
        }
        else if (context.canceled)
        {
            StopFiring();
            Debug.Log("Cancel");
        }
    }
}








