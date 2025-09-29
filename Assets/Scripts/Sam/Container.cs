using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

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

    private void Start()
    {
        rend = this.GetComponentInChildren<Renderer>();
        Resources_Container_Managment.Instance.addContainer(this);
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
       rend = this.GetComponentInChildren<Renderer>();

        Color originalColor = rend.material.color;

        rend.material.DOColor(Color.red, containerTurnRedIn)   
            .SetLoops(10, LoopType.Yoyo);          
            

        DOTween.Kill(originalColor);
    }
            
}
