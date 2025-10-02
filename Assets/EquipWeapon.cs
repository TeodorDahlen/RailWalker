using Unity.VisualScripting;
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


    [SerializeField]
    private GameObject pofAwayEffect;

    private AudioSource audioSource;
    private void Start()
    {
        equipped1 = true;
        equipped2 = true;
        audioSource = GetComponent<AudioSource>();
    }
    public void EquipGun1()
    {
        GameObject newEffect = Instantiate(pofAwayEffect, gun1.transform.position, Quaternion.identity);
        Destroy(newEffect, 1f);
        audioSource.Play();
        if (equipped1)
        {
            gun1.SetActive(false);
            equipped1 = false;
            gun1.GetComponent<GunBase>().reloadStarted = false;
      

            if (equipped2 == false)
            {
                EquipGun3();
            }
        }
        else
        {
            unEqiupGun3();
            equipped1 = true;
            gun1.SetActive(true);
        }
    }
    public void EquipGun2()
    {
        GameObject newEffect = Instantiate(pofAwayEffect, gun2.transform.position, Quaternion.identity);
        Destroy(newEffect, 1f);
        audioSource.Play();
        if (equipped2)
        {
            gun2.SetActive(false);
            equipped2 = false;
            gun2.GetComponent<GunBase>().reloadStarted = false;
            if (equipped1 == false)
            {
                EquipGun3();
            }
        }
        else
        {
            unEqiupGun3();
            equipped2 = true;
            gun2.SetActive(true);
        }
    }
    public void EquipGun3()
    {
        gatlingGun.SetActive(true);
    }

    public void unEqiupGun3()
    {
        gatlingGun.SetActive(false);
    }
}
