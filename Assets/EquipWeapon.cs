using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    private bool equipped1;
    private bool equipped2;
    private bool equipped3;

    [SerializeField]
    private GameObject gun1;
    [SerializeField]
    private GameObject gun2;

    [SerializeField]
    private GameObject gatlingGun;

    private void Start()
    {
        equipped1 = true;
        equipped2 = true;
    }
    public void EquipGun1()
    {
        if (equipped1)
        {
            gun1.SetActive(false);
            equipped1 = false;
            gun1.GetComponent<GunBase>().reloadStarted = false;
        }
        else
        {
            equipped1 = true;
            gun1.SetActive(true);
        }
    }
    public void EquipGun2()
    {
        if (equipped2)
        {
            gun2.SetActive(false);
            equipped2 = false;
            gun2.GetComponent<GunBase>().reloadStarted = false;
        }
        else
        {
            equipped2 = true;
            gun2.SetActive(true);
        }
    }
    //public void EquipGun3()
    //{
    //    if (equipped1)
    //    {
    //        gameObject.SetActive(false);
    //        equipped1 = false;
    //    }
    //    else
    //    {
    //        equipped1 = true;
    //        gameObject.SetActive(true);
    //    }
    //}
}
