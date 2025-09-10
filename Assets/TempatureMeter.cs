using UnityEngine;

public class TempatureMeter : MonoBehaviour
{

    [SerializeField]
    public float Heat;

    [SerializeField]
    private float reductionSpeed;

    [SerializeField]
    private GameObject pointer;

    public void AddHeat(float amount)
    {
        Heat += amount;
    }

    public void RemoveHeat(float amount)
    {
        Heat -= amount;
    }

    private void Update()
    {
        Heat -= Time.deltaTime * reductionSpeed;
        Heat = Mathf.Clamp(Heat, 0, 100);
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        // Map 0–100 heat to 0–180 degrees (adjust as you like)
        float angle = Heat * 1.8f;

        // Set rotation directly instead of rotating each frame
        pointer.transform.localRotation = Quaternion.Euler(0, 0, -angle); // Negative if clockwise
    }
}
