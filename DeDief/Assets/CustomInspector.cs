using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OfficeGenerator))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        OfficeGenerator office = (OfficeGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            office.Generate();
        }
    }
}
