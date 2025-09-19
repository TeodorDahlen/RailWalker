using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;

public class ShootTrans : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.LogWarning("I got shot");
        //issue here it wont go back is that it trigger multible times at ones so when you shoot for an example 2 bullets it does 2 close together
        //so it will move and wont go to the origin
    }

    [Button]
    private void test()
    {
        transform.DOPunchRotation(new Vector3(0, 0, -10), 0.5f, 10, 1f);
    }
          
}
