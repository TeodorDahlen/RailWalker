using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System;

public class Container : MonoBehaviour
{
    private int maxResourceCount = 10000;
    private int currentResourceCount = 0;
    private int damageResourceGone = 100;
    private float containerTurnRedIn = 0.8f;

    private Renderer rend;
    [SerializeField] private Color originalColor;

    private Health health;

    public bool GotResources = false;
    public bool amIdead = false;

    private GameObject containerGameObject;
    private Transform childVisualContainer;

    public Action showTrainDeadUI;
    [SerializeField]
    private GameObject managmentgameObject;
    [SerializeField]
    private UIGoneTrain UIGoneTrainScript;

    private void Start()
    {
       managmentgameObject = GameObject.Find("Managment");

       UIGoneTrainScript = managmentgameObject.GetComponent<UIGoneTrain>();

        rend = this.GetComponentInChildren<Renderer>();
        containerGameObject = this.gameObject;

        childVisualContainer = containerGameObject.transform.GetChild(0);

        if (childVisualContainer == null)
        {
            Debug.LogWarning("childvisual for container is null");
        }
      
        // Resources_Container_Managment.Instance.addContainer(this);
    }

    private void GetResources()
    {
        GotResources = true;
        currentResourceCount = maxResourceCount;
    }

    public int ReturnResourceCount()
    {
        return currentResourceCount;
    }

    public void TookDamage()
    {
        currentResourceCount = currentResourceCount - damageResourceGone;
        Debug.Log($"One container took damage");

        if (currentResourceCount <= 0)
        {
            GotResources = false;
            Debug.LogWarning("one container have no resources left");
        }
    }

    [Button]
    public void FlashRed()
    {
        if (UIGoneTrainScript != null)
        {
            UIGoneTrainScript.ShowCanvas();
        }

       rend = this.GetComponentInChildren<Renderer>();

        Color originalColor = rend.material.color;

        rend.material.DOColor(Color.red, containerTurnRedIn)   
            .SetLoops(10, LoopType.Yoyo);          
            

        DOTween.Kill(originalColor);
        Invoke("HideThisContainer", 2f);
    }

    private void HideThisContainer()
    {
        childVisualContainer.gameObject.SetActive(false);
        amIdead = true;
    }

    private void ShowThisContainer()
    {
        amIdead = false;
        childVisualContainer.gameObject.SetActive(true);
    }       
}