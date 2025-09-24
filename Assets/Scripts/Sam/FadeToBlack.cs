using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections;
using System;

public enum transis
{
    ToShovel,
    ToMech,
    nothing
}
public class FadeToBlack : MonoBehaviour
{
    public static FadeToBlack Instance;

    public OVRScreenFade screenFade;
    private int delayToLight = 3;
    public bool darkness = false;


    //transition to shovel sub is on shoottrans
    public Action transToShovel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (screenFade == null)
        {
            Debug.LogWarning("screenfade is null");
        }
    }

    [Button]
    public void FadeToDarkness(transis transis)
    {
        Debug.Log("fadetodarkness method");
        screenFade.FadeOut();
        if (transis == transis.ToShovel)
        {
            transToShovel?.Invoke();
        }
        darkness = true;
        Invoke("GoBackLight", delayToLight);
    }

    [Button]
    public void GoBackLight()
    {
        darkness = false;
        screenFade.FadeIn();
    }
}
