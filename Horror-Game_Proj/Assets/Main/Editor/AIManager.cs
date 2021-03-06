﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AIManager : EditorWindow {

    public GameObject room;
    public GameObject KeyRoom;
    public GameObject Floor;

    [MenuItem("AI/AI Manager")]


    public static void ShowWindow()
    {
        GetWindow(typeof(AIManager));
    }


    void OnGUI()
    {
        if (GUILayout.Button("Add Room"))
        {
            Instantiate(room);
        }

        if (GUILayout.Button("Add Key Room"))
        {
            if (GameObject.Find("Key Rooms") == null)
            {
                GameObject KeyRooms = new GameObject("Key Rooms");
            }

            Instantiate(KeyRoom, GameObject.Find("Key Rooms").transform);
        }

        if (GUILayout.Button("Add Floor"))
        {
            Instantiate(Floor);
        }
    }
}
