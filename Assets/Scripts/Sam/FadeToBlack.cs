using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using System.Collections;

public class FadeToBlack : MonoBehaviour
{
    public static FadeToBlack Instance;
    public OVRScreenFade screenFade;
    private int delayToLight = 3;
    public bool darkness = false;

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
    public void FadeToDarkness()
    {
        screenFade.FadeOut();
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
