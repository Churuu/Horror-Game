using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRoom : MonoBehaviour
{

    List<Transform> aiToPatrolPoint = new List<Transform>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ai>().switchAiPatrolPoint(aiToPatrolPoint);

        }
    }

    public void AddKeyPoint()
    {
        GameObject keyPoint = new GameObject("Point");
        keyPoint.transform.parent = this.gameObject.transform;
        aiToPatrolPoint.Add(keyPoint);
    }

    void OnDrawGizmos()
    {

        Gizmos.color = new Color(1f, 1f, 0f);
        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

    }
}
