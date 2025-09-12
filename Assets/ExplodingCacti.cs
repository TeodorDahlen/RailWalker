using UnityEngine;
using UnityEngine.Audio;

public class ExplodingCacti : MonoBehaviour
{
    [SerializeField]
    private GameObject explodingCactiEffect;
   
    public void Explode()
    {
        GameObject newExplosion = Instantiate(explodingCactiEffect,transform.position, Quaternion.identity);
        Destroy(newExplosion, 1.0f);

        TrainMovingTemp.Instance.everything.Remove(gameObject);
        Destroy(gameObject);
    }

    
}