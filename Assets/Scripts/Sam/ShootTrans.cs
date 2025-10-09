using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;

public class ShootTrans : MonoBehaviour
{
    private bool chekRotation = false;
    private bool teleportAfterRelease = false;  

    
    float xRotation;

    private GameObject Pivot;
    private Vector3 orgpos;
    private Quaternion orgRotation;

    private void Start()
    {
        Pivot = GameObject.Find("Pivot");
        //orgpos = transform.position;
        //orgRotation = transform.rotation;
        Invoke("UnsubscribeToEvents", 1f);
    }


    private void subscribeToEvents()
    {

        FadeToBlack.Instance.transToShovel += goToShovel;

    }

    private void UnsubscribeToEvents()
    {
        if (FadeToBlack.Instance != null)
        {
            FadeToBlack.Instance.transToShovel -= goToShovel;
            subscribeToEvents();
        }
        else
        {
            Debug.LogWarning("fadetoblack is null");
        }
    }

    public void holdingSpak()
    {
        chekRotation = true;
    }

    public void NotHoldingSpak()
    {
        chekRotation=false;
    }

    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chekRotation = true;
                
            /*
            if (!canBeShoot)
            {
                Debug.LogWarning("returning cant be shot flag");
                return;
            }
            Debug.Log("flag got shot");
            canBeShoot = false;

            transform.DOPunchRotation(new Vector3(0, 0, -10), 0.5f, 10, 1f).OnComplete(() => canBeShoot = true).OnComplete(()
                => FadeToBlack.Instance.FadeToDarkness(transis.ToShovel));

           
        }
    }
     */

    private void Update()
    {
        if (chekRotation)
        {
            CheckRotation();
        }

        if (chekRotation == false && teleportAfterRelease == true)
        {
            ActivateFadeToDarknessn();
        }
    }

    public void CheckRotation()
    {
         xRotation = transform.localEulerAngles.x;

        // convert unity's 0–360 range to -180–180
        //it is easier to read so its noe 360
        if (xRotation > 180)
            xRotation -= 360;

        //check if it's around -20 
        if (Mathf.Abs(xRotation - (-20f)) < 1f )
        {
            Debug.LogWarning("its around -20 degrees now");
            teleportAfterRelease = true;
        }

        if (Mathf.Abs(xRotation - (20f)) < 1f)
        {
            teleportAfterRelease = true;
        }
    }

    private void ActivateFadeToDarknessn()
    {
        FadeToBlack.Instance.FadeToDarkness(transis.ToShovel);
        teleportAfterRelease = false;
    }

    [Button]
    private void ResetPosition()
    {
       transform.rotation = Quaternion.identity;
    }
    

    private void goToShovel()
    {
        Debug.Log("darkness is true");
        
        StartCoroutine(DelayDark());
    }

    private IEnumerator DelayDark()
    {
        Debug.Log("go to shovel before dealy");
        yield return new WaitForSeconds(0.4f);
        TransitionManagment.Instance.ActivateShovel();
        ResetPosition();
        //canBeShoot = true;
        Debug.Log("go to Shovel");
    }
}
    
    
       
  

   


