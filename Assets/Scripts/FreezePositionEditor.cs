#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(FreezePosition))]
public class FreezePositionEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FreezePosition myScript = (FreezePosition)target;
        if (GUILayout.Button("Align"))
        {
            foreach (FreezePosition at in FindObjectsOfType<FreezePosition>())
            {
                at.Align();
            }
        }
    }
}
#endif