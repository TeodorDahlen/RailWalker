using UnityEngine;

public class Container : MonoBehaviour
{
    private int maxResourceCount = 10000;
    private int currentResourceCount = 0;
    private int damageResourceGone = 100;

    public bool GotResources = false;

    private void Start()
    {
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
}
