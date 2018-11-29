#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DrawLine))]
public class DrawLineEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DrawLine myScript = (DrawLine)target;
        if (GUILayout.Button("Draw Arc"))
        {
            myScript.DrawArc();
        }
    }
}
#endif
