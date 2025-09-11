using UnityEngine;

public class Test_getContainer : MonoBehaviour
{
    [SerializeField] private GameObject PreFabContainer;

    private void OnTriggerEnter(Collider other)
    {
      Debug.Log($"got touched by {other}");
        
        
      Instantiate(PreFabContainer, transform.position , Quaternion.identity);
      Debug.Log("spawning container");
        
    }
}
