using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using System;

public class ShootTrans : MonoBehaviour
{
    private bool canBeShoot = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canBeShoot)
        {
            return;
        }

        canBeShoot = false;

        transform.DOPunchRotation(new Vector3(0, 0, -10), 0.5f, 10, 1f).OnComplete(() => canBeShoot = true);

        TransitionManagment.Instance.ActivateShovel();
    }
          
}
