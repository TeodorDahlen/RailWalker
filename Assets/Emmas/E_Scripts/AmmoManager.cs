using UnityEngine;
using NaughtyAttributes;

//TODO: Find out why ammo is not added when heat is added from furnace
public class AmmoManager : MonoBehaviour
{
    public float amountToDeplate = 0.5f;
    [SerializeField] private TempatureMeter tempMeter;

    private float currentAmmo;
    private bool hasAmmo;
    void Start()
    {
        tempMeter = FindFirstObjectByType<TempatureMeter>();

        if (tempMeter == null)
        {
            Debug.LogError("TempatureMeter not found in the scene.");
            return;
        }

        currentAmmo = tempMeter.Heat;
        Debug.Log("Current Ammo: " + currentAmmo);
    }

    public bool HasAmmo()
    {
        currentAmmo = tempMeter.Heat;
        hasAmmo = currentAmmo > 0;
        return hasAmmo;
    }

    public void AddAmmo(float amount)
    {
        if (tempMeter != null)
        {
            tempMeter.AddHeat(amount);
            currentAmmo = amount;
            hasAmmo = true;
            Debug.Log("From AmmoManager: Ammo Added: " + amount + ". Current Ammo: " + tempMeter.Heat);
        }

    }

    public void DeplateAmmo(float amountToDeplate)
    {
        if (tempMeter != null)
        {
            tempMeter.RemoveHeat(amountToDeplate);
            currentAmmo = amountToDeplate;
            Debug.Log("Ammo Depleted by: " + amountToDeplate + ". Current Ammo: " + tempMeter.Heat);
        }
    }
}
