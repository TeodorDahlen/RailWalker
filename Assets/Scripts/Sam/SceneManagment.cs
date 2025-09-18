using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Oculus.Interaction.Context;

public class SceneManagment : MonoBehaviour
{
    public static SceneManagment Instance;

    private void Start()
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

    [Button]
    public void LoadMechScene()
    {
        SceneManager.LoadScene("TeodorScene");
    }

    [Button]
    public void LoadSamScene()
    {
        SceneManager.LoadScene("SamScene");
    }
}
