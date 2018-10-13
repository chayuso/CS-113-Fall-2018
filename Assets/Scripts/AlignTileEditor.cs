#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(AlignTile))]
public class AlignTileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        AlignTile myScript = (AlignTile)target;
        if (GUILayout.Button("Align"))
        {
            foreach (AlignTile at in FindObjectsOfType<AlignTile>())
            {
                at.Align();
            }
        }
    }
}
#endif