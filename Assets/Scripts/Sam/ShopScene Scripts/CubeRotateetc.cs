using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class CubeRotateetc : MonoBehaviour
{
    [SerializeField] private bool canBeShoot = true;
    [SerializeField] private GameObject ShowUpText;
    private Tween rotateTween;
    private Tween floatTween;

    private void Start()
    {
        ShowUpText.SetActive(false);

        rotateTween = transform.DORotate(new Vector3(0, 360, 0), 5f, RotateMode.FastBeyond360)
                 .SetRelative(true)
                 .SetEase(Ease.Linear)
                 .SetLoops(-1);

        floatTween = transform.DOMoveY(transform.position.y + 0.5f, 2f)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }

    public void KillBoxTweens()
    {
        rotateTween.Kill();
        floatTween.Kill();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bullet(Clone)" && canBeShoot)
        {
            canBeShoot = false;
            Debug.Log("pew sold");
        }

        Debug.LogWarning("TESTING");
    }

    [Button]
    private void testbuy()
    {
        Debug.Log("this is sold yaay");
        ShowUpText.SetActive(true);

        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
