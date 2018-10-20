#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(ItemAlign))]
public class ItemAlignEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ItemAlign myScript = (ItemAlign)target;
        if (GUILayout.Button("Align"))
        {
            myScript.Align();
        }
    }
}
#endif