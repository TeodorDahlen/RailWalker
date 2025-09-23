using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class ShadowDirection : MonoBehaviour
{
    public List<GameObject> AllShadows = new List<GameObject>();

    private Quaternion _lastRotation;

    void OnEnable()
    {
        // Initialize lastRotation so OnValidate can compare
        _lastRotation = transform.rotation;
        UpdateShadows();
    }

    void Update()
    {
        // Only run in Editor (not Play) when rotation changed
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            if (transform.rotation != _lastRotation)
            {
                _lastRotation = transform.rotation;
                UpdateShadows();
            }
        }
#endif
    }

    // Called when something is modified in the Inspector
    void OnValidate()
    {
        // Also update when inspector properties change
        UpdateShadows();
    }

    public void UpdateShadows()
    {
        AllShadows.Clear();
        ShadowObject[] newArray = FindObjectsOfType<ShadowObject>();
        foreach (ShadowObject obj in newArray)
        {
            AllShadows.Add(obj.gameObject);
        }
        foreach (GameObject obj in AllShadows)
        {
            obj.transform.rotation = transform.rotation;
        }

        // Mark scene dirty so changes are saved in editor
#if UNITY_EDITOR
        UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(gameObject.scene);
#endif
    }
}
