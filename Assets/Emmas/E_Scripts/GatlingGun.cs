using UnityEngine;
using NaughtyAttributes;
using static UnityEngine.InputSystem.InputAction;
using UnityEngine.XR.OpenXR.Input;
using Oculus.Haptics;

public class GatlingGun : MonoBehaviour
{
    [Header("Barrel Object")]
    [SerializeField] private Transform barrelTransform;

    [Header("Gatling Gun Settings")]
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float spinSpeed = 360f;
    [SerializeField] private Animation recoil;

// [SerializeField] private HapticSource hapticSource;

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
        return ammoManager.HasAmmo() && canFire;
    }

    [Button]
    public void ConstantFire()
    {
        canFire = CanShoot();
        canFire = true;
        if (!canFire)
        {
            Debug.Log("Out of Ammo, need coal");
            StopFiring();
        }
        else
        {
            if (shootingPoint != null)
            {
                InvokeRepeating(nameof(FireFromRotatingPoint), 0f, fireRate);
                InvokeRepeating(nameof(RotateBarrel), 0f, fireRate);
            }
            else
            {
                Debug.LogWarning("ShootingPoint not assigned on GatlingGun.");
            }
        }
    }

    private void RotateBarrel()
    {
        if (barrelTransform == null)
            return;

        barrelTransform.Rotate(new Vector3(0, 0, 1), spinSpeed * fireRate, Space.Self);
    }

    private void FireFromRotatingPoint()
    {
        if (shootingPoint == null) return;

        Shoot();
        if (recoil != null)
            recoil.Play();
    }

    [Button]
    public void StopFiring()
    {
        CancelInvoke(nameof(FireFromRotatingPoint));
        CancelInvoke(nameof(RotateBarrel));

        //if (shootingPoint != null)
        //    shootingPoint.transform.localPosition = originalLocalPos;
    }

    public void Shoot()
    {
        if (!CanShoot())
        {
            Debug.Log("StopFiring() triggered in Shoot(), need more coal");
            StopFiring();
            return;
        }

        // Consume furnace heat as ammo
        ammoManager.DepleteAmmo(ammoManager.amountToDeplete);

        // hapticSource?.Play();
        // Debug.Log("Pew Pew vibrate");

        // Fire bullet from pool
        GameObject bullet = bulletPool.GetGameObject();
        bullet.transform.position = shootingPoint.transform.position;
        bullet.transform.rotation = shootingPoint.transform.rotation;
        bullet.GetComponent<ReusableBullet>().direction = bullet.transform.forward;
        bullet.GetComponent<ReusableBullet>().SetPool(bulletPool);

        // Visual recoil
        if (recoil != null) recoil.Play();

        // Spawn muzzle flash VFX
        //GameObject newVFX = Instantiate(ShootingVFX, shootingPoint.transform.position,
        //    Quaternion.LookRotation(GetBulletDirection(shootingPoint.transform, spreadAngle)));
        //Destroy(newVFX, 0.5f);

        if (useRandomSpread)
        {
            bullet.transform.rotation = Quaternion.LookRotation(GetBulletDirection(shootingPoint.transform, spreadAngle));
            //newVFX.transform.rotation = Quaternion.LookRotation(GetBulletDirection(shootingPoint.transform, spreadAngle));
        }
    }

    public bool useRandomSpread = false;
    [SerializeField, Range(0, 90)] private float spreadAngle = 90f;

    public static Vector3 GetBulletDirection(Transform origin, float spread)
    {
        Vector3 dir = origin.forward;

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
