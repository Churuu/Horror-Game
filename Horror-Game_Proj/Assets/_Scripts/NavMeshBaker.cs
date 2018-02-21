using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour {

	public NavMeshSurface surface;

	public void BuildSurface()
	{
		surface.BuildNavMesh();
	}
}
