using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

    void OnDrawGizmos()
    {

        Gizmos.color = new Color(0f, 1f, 0f);
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().transform.localPosition, GetComponent<BoxCollider>().transform.localScale);

        Gizmos.color = new Color(0f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(GetComponent<BoxCollider>().transform.localPosition, GetComponent<BoxCollider>().transform.localScale);

    }
}
