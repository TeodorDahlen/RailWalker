using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOverScene : MonoBehaviour
{

    

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Any) || OVRInput.GetDown(OVRInput.Touch.Any))
        {
            Restart();
        }


    }

    private void Restart()
    {
        SceneManager.LoadScene("mainGameplayScene");
    }
}
