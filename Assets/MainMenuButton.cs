using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuButton : MonoBehaviour
{
    [SerializeField]
    private bool PlayButton;
    [SerializeField]
    private bool ExitButton;


    public void OnDestroy()
    {
        if (PlayButton)
        {
            SceneManager.LoadScene(3);
            return;
        }
        
        //if (ExitButton)
        //{
        //    Application.Quit();
        //    if (UnityEditor.EditorApplication.isPlaying)
        //    {
        //        UnityEditor.EditorApplication.isPlaying = false;
        //    }
        //    else if (Application.isPlaying)
        //    {
        //        Application.Quit();
        //    }
        //    return;
        //}
    }
}
