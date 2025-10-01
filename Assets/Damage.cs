using UnityEngine;

public class Damage : MonoBehaviour
{

    [SerializeField]
    public float damage;

    [SerializeField]
    private float damageToContainer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Health>() != null)
        {
            other.GetComponent<Health>().TakeDamage(damage);


            //make so the last container in list take damage
           var lastcontainer = Resources_Container_Managment.Instance.GetLastContainer();

            if (lastcontainer == null)
            {
                Debug.LogWarning("GAME OVER");
                return;
            }
            else
            {
                lastcontainer.gameObject.GetComponent<Health>().TakeDamage(damageToContainer);

            }
        }
    }
}
