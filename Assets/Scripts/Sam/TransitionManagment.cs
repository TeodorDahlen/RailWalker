using NaughtyAttributes;
using UnityEngine;

public class TransitionManagment : MonoBehaviour
{
    //This script will handle the transition between in train and the mech
    public Camera CenterEyeCamera;
    public Camera MechCamera;

   public GameObject FpsChar;


    void Start()
    {
        CenterEyeCamera.gameObject.SetActive(true);

        foreach (Transform child in FpsChar.transform)
        {
            child.gameObject.SetActive(false);
        }
        //just makes the camera on (im dying of the error if i have it on on the start )
        MechCamera.gameObject.GetComponent<Camera>().enabled = true;
        
    }

    [Button]
    private void MechActive()
    {
        CenterEyeCamera.gameObject.SetActive(false);

        foreach (Transform child in FpsChar.transform)
        {
            child.gameObject.SetActive(true);
        }

        
    }

    [Button]
    private void InsideTrain()
    {
        foreach (Transform child in FpsChar.transform)
        {
            child.gameObject.SetActive(false);
        }
        CenterEyeCamera.gameObject.SetActive(true);
    }
}
