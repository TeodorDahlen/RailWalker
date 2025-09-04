using UnityEngine;

public class GunBase : MonoBehaviour
{
    [SerializeField]
    private GameObject Projectile;

    [SerializeField]
    private GameObject ShootingVFX;

    [SerializeField]
    private GameObject ShootingPoint;


    public float radius = 0.5f;          // Radius of the sphere
    public float maxDistance = 1000f;       // How far the sphere travels
    public LayerMask hitLayers;          // Which layers to collide with



    //private void Update()
    //{
    //    Shoot();
    //}
    public void Shoot()
    {
        RaycastHit hit;
        Vector3 origin = ShootingPoint.transform.position;
        Vector3 direction = ShootingPoint.transform.forward;

        //Debug.DrawLine(origin, direction * maxDistance, Color.red, 2);
        
        if (Physics.SphereCast(origin, radius, direction, out hit, maxDistance, hitLayers))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
        ShootingPoint.transform.rotation = transform.rotation;
        Instantiate(Projectile, ShootingPoint.transform.position, ShootingPoint.transform.rotation);
    }
}