using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShadowDirection))]
public class ShadowDirectionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ShadowDirection sd = (ShadowDirection)target;

        if (GUILayout.Button("Update Shadows"))
        {
            sd.UpdateShadows();
        }
    }
}
