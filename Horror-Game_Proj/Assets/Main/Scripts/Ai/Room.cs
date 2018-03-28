using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    public bool roomEnabled = true;

    void OnDrawGizmos()
    {

        Gizmos.color = new Color(0f, 1f, 0f);
        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

    }
}
