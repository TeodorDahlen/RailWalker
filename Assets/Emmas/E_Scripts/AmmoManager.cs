using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] private List<TempatureMeter> tempMeter;
    public float amountToDeplete = 0.5f; // heat lost per shot

    public bool HasAmmo()
    {
        if (tempMeter != null)
        {
            foreach (var meter in tempMeter)
            {
                if (meter.Heat > 0)
                {
                    return true;
                }
            }
        }
        
        return false;
        // return tempMeter != null && tempMeter.Heat > 0;
    }

    public void DepleteAmmo(float amount)
    {
        if (tempMeter != null)
        {
            foreach (var meter in tempMeter)
            {

                meter.RemoveHeat(amount);
                // Debug.Log($"Ammo depleted by {amount}. Current Heat: {meter.Heat}");

            }
            // tempMeter.RemoveHeat(amount);
            // Debug.Log($"Ammo depleted by {amount}. Current Heat: {tempMeter.Heat}");
        }
    }
}
