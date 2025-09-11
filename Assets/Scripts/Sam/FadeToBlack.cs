using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public static FadeToBlack Instance;
    public OVRScreenFade screenFade;

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
    }

    public void FadeToDarkness()
    {
        screenFade.FadeOut();
    }

    public void GoBackLight()
    {

        screenFade.FadeIn();
    }
}
