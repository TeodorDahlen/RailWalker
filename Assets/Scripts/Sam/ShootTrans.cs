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
        FadeToBlack.Instance.transToShovel -= goToShovel;
        FadeToBlack.Instance.transToShovel += goToShovel;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!canBeShoot)
        {
            return;
        }

        canBeShoot = false;

        transform.DOPunchRotation(new Vector3(0, 0, -10), 0.5f, 10, 1f).OnComplete(() => canBeShoot = true).OnComplete(()
            => FadeToBlack.Instance.FadeToDarkness(transis.ToShovel));
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
    
    
       
  

   


