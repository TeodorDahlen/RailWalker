using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;


public class Shovel : MonoBehaviour
{
    [SerializeField]
    private Vector3 LocalorgPos;
    [SerializeField]
    private Quaternion LocalOrgRotation;
    
    [SerializeField]
    private bool HasCoal;

    [SerializeField]
    private GameObject CoalOnShovel;

    private Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();

        LocalorgPos = transform.localPosition;
        LocalOrgRotation = transform.localRotation;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Coal>() != null && HasCoal == false)
        {
            HasCoal = true;
            CoalOnShovel.SetActive(true);
        }
        else if(other.GetComponent<Furnace>() != null && HasCoal == true)
        { 
            HasCoal = false;
            CoalOnShovel.SetActive(false);
            other.GetComponent<Furnace>().AddCoal();
        }
    }

    
    public void SnapBack()
    {
        Rigidbody.linearVelocity = new Vector3(0f, 0f, 0f);
        Rigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
        transform.localPosition = LocalorgPos;
        transform.localRotation = LocalOrgRotation;
    }
}
