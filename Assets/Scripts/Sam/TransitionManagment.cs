using NaughtyAttributes;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.Events;
using static Oculus.Interaction.TransformerUtils;
using NaughtyAttributes;

public class TransitionManagment : MonoBehaviour
{
    //This script will handle the transition between in train and the mech
    public static TransitionManagment Instance;

    public GameObject choChoSpak;
    public Transform chochoTransform;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    GameObject presser;
    AudioSource sound;
    bool isPressed;

    private double maxconstrain = 0.1;
    private double stopHere;
    private void Start()
    {
        isPressed = false;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        stopHere = chochoTransform.transform.position.y - 0.1;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            if (chochoTransform.transform.position.y <= stopHere)
            {
                // isPressed = other.gameObject;
                //onPress.Invoke();
                //sound.Play();
                //isPressed = true;
                Debug.Log("hello");
            }
          
        }
    }

    private void Update()
    {
        if (chochoTransform.transform.position.y <= stopHere)
        {
            // isPressed = other.gameObject;
            //onPress.Invoke();
            //sound.Play();
            //isPressed = true;
            Debug.Log("Mech time pew");
        }
    }


    [Button]
    private void thing()
    {
        //world pos + position constrains
        Debug.Log($"possition of chocho is: {chochoTransform.transform.position}");

        Debug.Log($"will stop at possition: {chochoTransform.transform.position.y - 0.1}");
    }




}
