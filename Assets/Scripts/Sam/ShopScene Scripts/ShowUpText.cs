using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
public class ShowUpText : MonoBehaviour
{
    Vector3 orgpos;
    private void Start()
    {
        orgpos = transform.position;
    }

    [Button]
    public void SoldMove()
    {
        transform.DOMoveY(2f, 5f);
    }
}
