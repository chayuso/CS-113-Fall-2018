#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ArcSwap))]
public class ArcSwapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ArcSwap myScript = (ArcSwap)target;
        if (GUILayout.Button("ToggleArcRange"))
        {
            myScript.SwapState();
        }
    }
}
#endif