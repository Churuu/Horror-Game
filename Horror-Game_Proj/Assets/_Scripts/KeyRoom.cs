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
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().transform.localPosition, GetComponent<BoxCollider>().transform.localScale);

        Gizmos.color = new Color(1f, 1f, 0f, 0.4f);
        Gizmos.DrawCube(GetComponent<BoxCollider>().transform.localPosition, GetComponent<BoxCollider>().transform.localScale);

    }


}
