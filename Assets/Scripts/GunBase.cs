using NUnit.Framework;
using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;
    [SerializeField]
    private GameObject AutoProjectile;

    [SerializeField]
    private GameObject ShootingVFX;

    [SerializeField]
    private GameObject ShootingPoint;

    public float radius = 5f;
    [HideInInspector]
    public float maxDistance = 10000f;
    public LayerMask hitLayers;
    public LayerMask targetLayer;

    [HideInInspector]
    public List<GameObject> DeadTarget = new List<GameObject>();
    private GameObject target;
    private GameObject lastTarget;

    [SerializeField]
    private GameObject targetMarker;
    private Vector3 baseScale;
    private Vector3 mainScale;
    [SerializeField]
    private Transform playerCamera;

    [SerializeField]
    private float distanceFromCamera = 2f;

    private bool newTarget;

    [Space]
    [Header("Reload Stuff")]
    [SerializeField]
    private int maxBullets = 6;
    private int currentBullets;

    [SerializeField]
    private float reloadSpeed = 3.0f;

    [SerializeField]
    private GameObject reloadVFX;

    private AudioSource audioSource;

    //[SerializeField]
    //private AudioClip clickAudio;

    [SerializeField]
    private GameObject reloadSpot;

    private bool reloadStarted;

    [SerializeField]
    private AudioClip reloadSound;

    [Space]
    [Header("Ammo Stuff")]
    [SerializeField]
    private List<GameObject> AmmoNumbers = new List<GameObject>();

    [Header("Animation Stuff")]
    [SerializeField]
    private Animator animator;


    private void Start()
    {
        baseScale = targetMarker.transform.localScale;
        currentBullets = maxBullets;
        audioSource = GetComponent<AudioSource>();
        reloadStarted = false;
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 origin = ShootingPoint.transform.position;
        Vector3 direction = ShootingPoint.transform.forward;

        if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance, hitLayers))
        {
            if (hit.collider.GetComponent<Health>() != null && !DeadTarget.Contains(hit.collider.gameObject))
            {
                target = hit.collider.gameObject;
            }
        }
        else
        {
            lastTarget = null;
            target = null;
        }

        mainScale = Vector3.Lerp(mainScale, baseScale, Time.deltaTime * 15);
        if (target != null)
        {
            if(target != lastTarget || lastTarget == null)
            {
                newTarget = true;
            }

            lastTarget = target;
            targetMarker.SetActive(true);
            Vector3 enemyDirection = (target.GetComponent<Health>().GetCore().transform.position - playerCamera.transform.position).normalized;
            targetMarker.transform.position = Vector3.Lerp(targetMarker.transform.position, playerCamera.transform.position - (enemyDirection * -distanceFromCamera), Time.deltaTime * 20);
            if (mainScale.magnitude >= baseScale.magnitude * 0.8f || !newTarget)
            {
                targetMarker.transform.localScale = Vector3.Lerp(targetMarker.transform.localScale, baseScale * 0.25f, Time.deltaTime * 10);
                newTarget = false;
                mainScale = targetMarker.transform.localScale;
            }
            else
            {
                targetMarker.transform.localScale = mainScale;
            }
            targetMarker.transform.LookAt(playerCamera.transform.position);
            
        }
        else
        {
            targetMarker.transform.localScale = Vector3.Lerp(targetMarker.transform.localScale, Vector3.zero, Time.deltaTime * 10);

            Vector3 forwardDirection = transform.forward;
            targetMarker.transform.position = Vector3.Lerp(targetMarker.transform.position, playerCamera.transform.position - (forwardDirection * -distanceFromCamera * 0.6f), Time.deltaTime * 10);
        }

    }
    public void Shoot()
    {
        if (currentBullets <= 0)
        {
            if (reloadStarted == false)
            {
                StartCoroutine(Reload());
                reloadStarted = true;
            }
            GameObject newVfx = Instantiate(reloadVFX, reloadSpot.transform.position, reloadSpot.transform.rotation);
            newVfx.GetComponent<EffectLookAtPlayer>().Target = Camera.main.gameObject;
            Destroy(newVfx, 1.0f);
            //audioSource.PlayOneShot(clickAudio);
           
        }
        else
        {
            currentBullets--;
            animator.SetTrigger("Fire");
            UpdateAmmoCounter();
            if (target != null)
            {
                Health hp = target.GetComponent<Health>();
                if (hp.GetHealth() <= 10)
                {
                    DeadTarget.Add(hp.gameObject);
                    hp.gameObject.layer = LayerMask.NameToLayer("Default");
                    targetMarker.transform.localScale = baseScale * 1.25f;
                }

                Vector3 AutoAimDirection = hp.GetCore().transform.position - ShootingPoint.transform.position;
                ShootingPoint.transform.forward = AutoAimDirection.normalized;
                GameObject newBullet = Instantiate(AutoProjectile, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
                newBullet.GetComponent<AutoBullet>().target = hp.GetCore();

                GameObject newVFX2 = Instantiate(ShootingVFX, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
                Destroy(newVFX2, 0.5f);
                return;

            }
            ShootingPoint.transform.rotation = transform.rotation;
            Instantiate(Projectile, ShootingPoint.transform.position, ShootingPoint.transform.rotation);

            GameObject newVFX = Instantiate(ShootingVFX, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
            Destroy(newVFX, 0.5f);
            if(reloadStarted == true)
            {
                reloadStarted = false;
            }
        }
    }


    private IEnumerator Reload()
    {
        audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadSpeed);
        currentBullets = maxBullets;
        UpdateAmmoCounter();
        reloadStarted = false;
    }

    private void UpdateAmmoCounter()
    {
        switch (currentBullets)
        {
            case 0:
                AmmoNumbers[0].SetActive(false);
                AmmoNumbers[6].SetActive(true);
                break;
            case 1:
                AmmoNumbers[1].SetActive(false);
                AmmoNumbers[0].SetActive(true);
                break;
            case 2:
                AmmoNumbers[2].SetActive(false);
                AmmoNumbers[1].SetActive(true);
                break;
            case 3:
                AmmoNumbers[3].SetActive(false);
                AmmoNumbers[2].SetActive(true);
                break;
            case 4:
                AmmoNumbers[4].SetActive(false);
                AmmoNumbers[3].SetActive(true);
                break;
            case 5:
                AmmoNumbers[5].SetActive(false);
                AmmoNumbers[4].SetActive(true);
                break;
            case 6:
                AmmoNumbers[6].SetActive(false);
                AmmoNumbers[5].SetActive(true);
                break;
            default:
                break;
        }
    }
}

