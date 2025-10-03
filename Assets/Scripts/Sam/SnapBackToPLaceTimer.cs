using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering;


public class SnapBackToPLaceTimer : MonoBehaviour
{
    [SerializeField]
    private Shovel ShovelScript;

    [SerializeField]
    private GatlingGun GatlingGunScript;

    [SerializeField]
    private GameObject gatlingGrab;

    private bool Shovel = false;

    [SerializeField]
    private Vector3 gatlingOrgPos;
    [SerializeField]
    private Quaternion gatlingOrgRot;
    private void Start()
    {
        if (transform.parent.gameObject.CompareTag("Shovel") == true )
        {
            Shovel = true;
        }
        else
        {
            Shovel = false;
            gatlingOrgPos = gatlingGrab.transform.position;
            gatlingOrgRot = gatlingGrab.transform.rotation;
           
        }
    }

    public void TimerOn()
    {
        Invoke("SnapBackToOrg", 3f);
    }

    private void SnapBackToOrg()
    {
        if (Shovel)
        {
            ShovelScript.SnapBack();
            Debug.Log("Shovel snap back to original position");
        } else
        {
            gatlingGrab.transform.position = gatlingOrgPos;
            gatlingGrab.transform.rotation = gatlingOrgRot;
            Debug.Log("gatling gun snap back to original position");
        }
        
    }

    public void CancelSnapBack()
    {
        CancelInvoke("SnapBackToOrg");
    }

    
}
