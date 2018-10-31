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
        myScript.Align();

    }
}
#endif