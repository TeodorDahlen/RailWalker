using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMappingManager : MonoBehaviour
{
     public InputActionAsset inputActions;
    //controller tracking left and right
    public GameObject activateWhenShovel;
    public GameObject activateWhenShovel2;
    
    private InputActionMap shootMap;
    private InputActionMap shovelInteractMap;

    //DEN HÄR SKA LYSSNA
    //denna ska sub till från transitionmanager

    private void Start()
    {
        shootMap = inputActions.FindActionMap("Game");
        shovelInteractMap = inputActions.FindActionMap("Interact");

        shootMap.Enable();
        shovelInteractMap.Disable();

        Invoke("UnsubscribeToEvents", 1f);
    }

    private void subscribeToEvents()
    {
        //subscribing event
        Debug.Log("Subscribing... Instance is: " + TransitionManagment.Instance);

        TransitionManagment.Instance.changeControlMapToMech += MechTime;
        TransitionManagment.Instance.changeControlMapToShovel += ShovelTime;
    }

    private void UnsubscribeToEvents()
    {
          TransitionManagment.Instance.changeControlMapToShovel -= ShovelTime;
          TransitionManagment.Instance.changeControlMapToMech -= MechTime;

          subscribeToEvents();
    }

    [Button]
    private void ShovelTime()
    {
        Debug.LogWarning("shovel time changing interact map");
        shootMap.Disable();
        shovelInteractMap.Enable();

        activateWhenShovel.SetActive(true);
        activateWhenShovel2.SetActive(true);


        //foreach (Transform child in activateWhenShovel.transform)
        //{
        //    child.gameObject.SetActive(true);
        //}

        //foreach (Transform child in activateWhenShovel2.transform)
        //{
        //    child.gameObject.SetActive(true);
        //}
    }

    [Button]
    private void MechTime()
    {
        Debug.LogWarning("Mech time changing interact map");

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
