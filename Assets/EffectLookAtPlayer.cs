using UnityEngine;

public class EffectLookAtPlayer : MonoBehaviour
{
    [HideInInspector]
    public GameObject Target;

    private void Start()
    {
        Target = TrainMovingTemp.Instance.gameObject;
        transform.LookAt(Target.transform.position);
        transform.Rotate(0, 180f, 0);
    }
}
