using UnityEngine;

public class FakeShadow : MonoBehaviour
{
    [SerializeField] private GameObject shadowPrefab; // Quad or sprite with shadow texture
    [SerializeField] private float maxDistance = 1000f; // How far down to check
    [SerializeField] private LayerMask groundLayer; // What counts as "ground"
    [SerializeField] private float positionLerpSpeed = 10f; // How fast the shadow moves
    [SerializeField] private float scaleLerpSpeed = 10f; // How fast the shadow scales
    [SerializeField] private float rotationLerpSpeed = 10f; // How fast the shadow rotates

    private void Update()
    {
        UpdateShadow();
    }

    private void UpdateShadow()
    {
        if (!shadowPrefab) return;

        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, groundLayer))
        {
            shadowPrefab.SetActive(true);

            // Target position slightly above the ground
            Vector3 targetPos = hit.point + Vector3.up * 0.01f;
            shadowPrefab.transform.position = Vector3.Lerp(shadowPrefab.transform.position, targetPos, positionLerpSpeed * Time.deltaTime);

            // Target rotation aligned to the ground normal
            Quaternion targetRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
            shadowPrefab.transform.rotation = Quaternion.Slerp(shadowPrefab.transform.rotation, targetRot, rotationLerpSpeed * Time.deltaTime);

            // Target scale based on distance
            float distance = Vector3.Distance(transform.position, hit.point);
            float targetScale = Mathf.Clamp(1 / (distance + 1), 0.2f, 1f);
            Vector3 scale = new Vector3(targetScale, targetScale, targetScale);
            shadowPrefab.transform.localScale = Vector3.Lerp(shadowPrefab.transform.localScale, scale, scaleLerpSpeed * Time.deltaTime);
        }
        else
        {
            shadowPrefab.SetActive(false);
        }
    }
}
