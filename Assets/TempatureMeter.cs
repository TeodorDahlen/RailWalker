using UnityEngine;
using NaughtyAttributes;

public class TempatureMeter : MonoBehaviour
{

    [SerializeField]
    public float Heat;

    [SerializeField]
    private float reductionSpeed;

    [SerializeField]
    private GameObject pointer;
    private AmmoManager ammoManager;
    

    void Start()
    {
        ammoManager = FindFirstObjectByType<AmmoManager>();
    }

    // [Button]
    // public void SetHeat()
    // {
    //     float debugAmount = 10f;
    //     AddHeat(debugAmount);
    // }

    public void AddHeat(float amount)
    {
        if (Heat >= 100)
        {
            Debug.Log("Heat is already at maximum.");
            return;
        }

        amount = 10;
        Heat += amount;
        ammoManager.AddAmmo(amount);
    }

    public void RemoveHeat(float depletedAmmo)
    {
        depletedAmmo = ammoManager.amountToDeplate;
        Heat -= depletedAmmo;
    }

    private void Update()
    {
        Heat -= Time.deltaTime * reductionSpeed;
        Heat = Mathf.Clamp(Heat, 0, 100);
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        // Map 0�100 heat to 0�180 degrees (adjust as you like)
        float angle = Heat * 1.8f;

        // Set rotation directly instead of rotating each frame
        pointer.transform.localRotation = Quaternion.Euler(0, 0, -angle); // Negative if clockwise
    }
}
