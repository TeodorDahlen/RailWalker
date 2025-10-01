using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System;
using System.Collections;

public class ShootTrans : MonoBehaviour
{
   [SerializeField] private bool canBeShoot = true;

    private void Start()
    {
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


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
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
        canBeShoot = true;
        Debug.Log("go to Shovel");
    }
}
    
    
       
  

   


