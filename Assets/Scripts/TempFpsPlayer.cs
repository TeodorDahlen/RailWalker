using UnityEngine;

public class TempFpsPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float mouseSensitivity = 2f;

    private float verticalLookRotation;
    private Transform playerCamera;

    void Start()
    {
        // Get reference to the first camera found under the player
        playerCamera = GetComponentInChildren<Camera>().transform;

        // Lock cursor to center of screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // --- Mouse Look ---
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate player left/right
        transform.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down
        verticalLookRotation -= mouseY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0f, 0f);

        // --- Movement ---
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.right * moveX + transform.forward * moveZ).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime; //ChatGPT
    }
}

