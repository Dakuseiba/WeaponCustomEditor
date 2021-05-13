using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Weapons))]
public class WeaponsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Window"))
        {
            WeaponsEditorWindow.Open((Weapons)target);
        }
        base.OnInspectorGUI();
    }
}
