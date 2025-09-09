using UnityEngine;

public class Shovel : MonoBehaviour
{

    [SerializeField]
    private bool HasCoal;

    [SerializeField]
    private GameObject CoalOnShovel;

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
}
