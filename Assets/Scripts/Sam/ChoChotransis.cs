using Oculus.Platform.Models;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ChoChotransis : MonoBehaviour
{
    [SerializeField] bool isPressed = false;
    public UnityEvent onPress;
    public float orgposY;
    [SerializeField] float stopHere;

    private void Start()
    {
        StartCoroutine(Delay());
    }

     private IEnumerator Delay()
     {
        yield return new WaitForSeconds(3f);
        orgposY = transform.localPosition.y;
        stopHere = orgposY - 0.1f;
     }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            if (transform.position.y <= stopHere)
            {
                FadeToBlack.Instance.FadeToDarkness(transis.ToMech);
                //sound.Play();
                isPressed = true;
                Debug.Log("hello: " + transform.localPosition.y);

                if (FadeToBlack.Instance.darkness == true)
                {
                    Debug.Log("darkness is true");
                    StartCoroutine(DelayDark());
                }
            }
        }
    }
    private IEnumerator DelayDark()
    {
        Debug.Log("go to mech before dealy");
        yield return new WaitForSeconds(0.4f);
        onPress.Invoke();

        Debug.Log("go to mech");
    }

    private void OnTriggerExit(Collider other)
    {
        if (isPressed == true)
        {
            isPressed = false;
        }
    }


    //float draNerMigIHelvetet = 100f;
    //private void Update()
    //{
    //    if (transform.localPosition.y < draNerMigIHelvetet)
    //    {
    //        draNerMigIHelvetet = transform.localPosition.y;
    //        Debug.Log("draNerMigIHelvetet: " + draNerMigIHelvetet);
    //    }
    //}
}
