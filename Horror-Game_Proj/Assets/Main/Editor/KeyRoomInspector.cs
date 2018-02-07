using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(KeyRoom))]
public class KeyRoomInspector : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		KeyRoom keyRoom = (KeyRoom)target;
		if(GUILayout.Button("Add Keypoint"))
		{
			keyRoom.AddKeyPoint();
		}

		if(GUILayout.Button("Remove Keypoint"))
		{
			keyRoom.RemoveKeypoint();
		}
	}
}
