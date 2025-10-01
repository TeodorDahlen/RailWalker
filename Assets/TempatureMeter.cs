using UnityEngine;

public class TempatureMeter : MonoBehaviour
{
    [SerializeField] public float Heat;
    [SerializeField] private float reductionSpeed;
    [SerializeField] private GameObject pointer;

    public void AddHeat(float amount)
    {
        if (Heat >= 100)
        {
            Debug.Log("Heat is already at maximum.");
            return;
        }

        Heat += amount;
        Heat = Mathf.Clamp(Heat, 0, 100);
        Debug.Log($"Heat added: {amount}. Current Heat: {Heat}");
    }

    public void RemoveHeat(float amount)
    {
        Heat -= amount;
        Heat = Mathf.Clamp(Heat, 0, 100);
    }

    private void Update()
    {
        Heat -= Time.deltaTime * reductionSpeed;
        Heat = Mathf.Clamp(Heat, 0, 100);

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        // Map 0–100 heat to 0–180 degrees rotation
        float angle = Heat * 1.8f;
        pointer.transform.localRotation = Quaternion.Euler(0, 0, -angle);
    }
}
