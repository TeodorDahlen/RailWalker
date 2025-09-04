using UnityEngine;
using UnityEngine.InputSystem;

public class ShovelGrab : MonoBehaviour, IGrabbableObject
{
    private InputAction grabAction;
    private bool isGrabbing = false;

    void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        grabAction = playerInput.actions["Grab"];
        grabAction.performed += ctx => OnGrab();
        grabAction.canceled += ctx => OnRelease();

        if (grabAction == null)
        {
            Debug.LogError("Grab action not found in PlayerInput actions.");
        }
    }

    private void OnEnable()
    {
        grabAction.Enable();
    }

    private void OnDisable()
    {
        grabAction.Disable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered shovel area");
            CanGrab();
            // Optionally, provide feedback to the player
        }
    }

    public void OnGrab()
    {
        Debug.Log("Grabbed");
        // Handle grab logic
    }

    public void OnRelease()
    {
        // Handle release logic
    }

    public bool CanGrab()
    {
        // Determine if the object can be grabbed
        Debug.Log("Can Grab set to:" + CanGrab());
        return true;
    }

    public bool TwoHanded()
    {
        // Determine if the object requires two hands
        return false;
    }

    void Update()
    {
        
    }

}
