using NaughtyAttributes;
using UnityEngine;

public class Test_getContainer : MonoBehaviour
{
    [SerializeField] private GameObject PreFabContainer;

    private void OnTriggerEnter(Collider other)
    {
      Debug.Log($"got touched by {other}");
        
        
     if (FadeToBlack.Instance.darkness == true)
        {
            SpawnContainer();
        }
     else
        {
            SpawnContainer();
        }

      Debug.Log("spawning container");
      FadeToBlack.Instance.FadeToDarkness();
        
    }

    [Button]
    private void SpawnContainer ()
    {
       Instantiate(PreFabContainer, transform.position, Quaternion.identity);
    }
}
