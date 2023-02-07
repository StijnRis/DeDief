using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OfficeGenerator))]
public class CustomInspectorOfficeGenerator : Editor
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

[CustomEditor(typeof(RoomGenerator), true)]
public class CustomInspectorRoomGenerator : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        RoomGenerator room = (RoomGenerator)target;
        if (GUILayout.Button("Generate"))
        {
            room.Generate();
        }
    }
}

