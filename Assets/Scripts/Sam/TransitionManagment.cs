using NaughtyAttributes;
using NaughtyAttributes;
using Oculus.Interaction;
using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using static Oculus.Interaction.TransformerUtils;

public class TransitionManagment : MonoBehaviour
{
    //This script will handle the transition between in train and the mech
    public static TransitionManagment Instance;

    //will need to get players transform for to be able to change the position


    [SerializeField] Transform mechCameraRigPosition, shovelCameraRigPosition;

    // public GameObject cameraShovel;
    // public GameObject cameraMech;
    public Transform cameraRig;
    public GameObject gunInHand;
    public GameObject gunInHand2;

    public GameObject gunPewInteract;
    public GameObject shovelInteract;

    private Vector3 mechposition;
    public Vector3 shovelPos;

    GameObject presser;
    AudioSource sound;

    private double maxconstrain = 0.1;
   [SerializeField] private float stopHere;

    //ACTION EVENTS
    //subscrib is in controllermappingmanager
    public event Action mechTransis;
    public event Action shovelTransis;

    public float orgpos;

    private void Start()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        /*
        foreach (Transform child in cameraShovel.transform)
        {
            child.gameObject.SetActive(false);
        }
        */

        gunPewInteract.gameObject.SetActive(true);
        shovelInteract.gameObject.SetActive(false);

    }

    [Button]
    public void ActivateMech()
    {
        gunPewInteract.gameObject.SetActive(true);
        shovelInteract.gameObject.SetActive(false);

        mechTransis?.Invoke();
        // cameraRig.transform.position = mechposition;


        cameraRig.transform.position = mechCameraRigPosition.position;


        ActivatePewPew();
    }

    [Button]
    public void ActivateShovel()
    {
        gunPewInteract.gameObject.SetActive(false);
        shovelInteract.gameObject.SetActive(true);
        shovelTransis?.Invoke();

        DeactivatePewPew();

        // mechposition = cameraMech.transform.position;

        // shovelPos = cameraShovel.transform.position;

        // cameraRig.transform.position = shovelPos;

        cameraRig.transform.position = shovelCameraRigPosition.position;
    }

    public void ActivatePewPew()
    {
        gunInHand.gameObject.SetActive(true);
        gunInHand2.gameObject.SetActive(true);
        //gunPew.gameObject.SetActive(true);
    }

    public void DeactivatePewPew ()
    {
        gunInHand.gameObject.SetActive(false);
        gunInHand2.gameObject.SetActive(false);
        //gunPew.gameObject.SetActive(false);
    }

}
