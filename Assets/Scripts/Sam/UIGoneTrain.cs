using NaughtyAttributes;
using UnityEngine;

public class UIGoneTrain : MonoBehaviour
{
    public Camera ContainerCamera; // your secondary camera
    public GameObject popupContainerCanvas; // your Canvas or Raw Image

    private void Start()
    {
        popupContainerCanvas.SetActive(false);
        ContainerCamera.enabled = false;
    }

    [Button]
    public void ShowCanvas()
    {
        popupContainerCanvas.SetActive(true);
        ContainerCamera.enabled = true;
    }

    
    [Button]
    public void HideCanvas()
    {
        popupContainerCanvas.SetActive(false); 
        ContainerCamera.enabled = false; 
    }
} 

