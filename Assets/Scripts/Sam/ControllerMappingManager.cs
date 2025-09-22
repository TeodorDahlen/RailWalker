using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMappingManager : MonoBehaviour
{
     public InputActionAsset inputActions;
    public GameObject activateWhenShovel;
    public GameObject activateWhenShovel2;


    
    private InputActionMap shootMap;
    private InputActionMap shovelInteractMap;



    private void Start()
    {
        shootMap = inputActions.FindActionMap("Game");
        shovelInteractMap = inputActions.FindActionMap("Interact");

        shootMap.Enable();
        shovelInteractMap.Disable();
    }

    [Button]
    private void ShovelTime()
    {
        shootMap.Disable();
        shovelInteractMap.Enable();

        foreach (Transform child in activateWhenShovel.transform)
        {
            child.gameObject.SetActive(true);
        }

        foreach (Transform child in activateWhenShovel2.transform)
        {
            child.gameObject.SetActive(true);
        }
    }

    [Button]
    private void MechTime()
    {
        shootMap.Enable();
        shovelInteractMap.Disable();

        foreach (Transform child in activateWhenShovel.transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in activateWhenShovel2.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
