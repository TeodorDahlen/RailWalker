using NUnit.Framework;
using Oculus.Platform;
using System.Collections.Generic;
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
    public float maxDistance = 10000f;
    public LayerMask hitLayers;
    public LayerMask targetLayer;

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
    private void Start()
    {
        baseScale = targetMarker.transform.localScale;
    }
    private void Update()
    {
        RaycastHit hit;
        Vector3 origin = ShootingPoint.transform.position;
        Vector3 direction = ShootingPoint.transform.forward;

        //Debug.DrawLine(origin, direction * maxDistance, Color.red, 2);

        if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance, hitLayers))
        {
            if (hit.collider.GetComponent<Health>() != null && !DeadTarget.Contains(hit.collider.gameObject))
            {
                target = hit.collider.gameObject;
                //target.layer = LayerMask.NameToLayer("Target");
            }
        }
        else
        {
            lastTarget = null;
            target = null;
        }

        //if(target = null)
        //{ 
        //    if (Physics.SphereCast(origin, radius * 2, direction, out hit, maxDistance, targetLayer))
        //    {
        //        target = lastTarget;
        //    }
        //    else
        //    {
        //        lastTarget.layer = LayerMask.NameToLayer("Enemy");
        //        lastTarget = null;
        //    }
        //}

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

    }
}

