using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRoom : MonoBehaviour
{

    public Transform[] aiToPatrolPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ai>().switchAiPatrolPoint(aiToPatrolPoint);

        }
    }

    void OnDrawGizmos()
    {

        Gizmos.color = new Color(1f, 1f, 0f);
        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

    }
}
