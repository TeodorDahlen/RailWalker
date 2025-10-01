using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] private TempatureMeter tempMeter;
    public float amountToDeplete = 0.5f; // heat lost per shot

    public bool HasAmmo()
    {
        return tempMeter != null && tempMeter.Heat > 0;
    }

    public void DepleteAmmo(float amount)
    {
        if (tempMeter != null)
        {
            tempMeter.RemoveHeat(amount);
            Debug.Log($"Ammo depleted by {amount}. Current Heat: {tempMeter.Heat}");
        }
    }
}
