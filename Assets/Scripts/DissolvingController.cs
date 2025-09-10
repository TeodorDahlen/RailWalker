using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
public class DissolvingController : MonoBehaviour
{

    [SerializeField]
    private Material material;


    
    [SerializeField]
    private float dissolveRate = 0.0125f;

    [SerializeField]
    private float refershRate = 0.025f;

    private void Start()
    {

        StartCoroutine(DissolveCo());
    }
  
    public IEnumerator DissolveCo()
    {
        float counter = 0;
        while (counter < 1f)
        {
            counter += dissolveRate;
            material.SetFloat("_DisolveAmount", counter);

            yield return new WaitForSeconds(refershRate);
        }
    }
}
