using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyRoom : MonoBehaviour
{

    public List<Transform> aiToPatrolPoint = new List<Transform>();

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FindObjectOfType<Ai>().switchAiPatrolPoint(aiToPatrolPoint);

        }
    }

    public void AddKeyPoint()
    {
        GameObject point = new GameObject("Point");
        point.transform.position = transform.position;
        point.transform.parent = gameObject.transform;
        point.tag = "Point";
        aiToPatrolPoint.Add(point.transform);
    }

    public void RemoveKeypoint()
    {
        if(aiToPatrolPoint.Count > 0)
        {
            var lastObject = aiToPatrolPoint[aiToPatrolPoint.Count - 1];
            aiToPatrolPoint.RemoveAt(aiToPatrolPoint.Count - 1);
            DestroyImmediate(lastObject.gameObject);
        }else{
            Debug.LogWarning("No keypoint to remove");
        }

    }

    void OnDrawGizmos()
    {

        Gizmos.color = new Color(1f, 1f, 0f);
        Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

        Gizmos.color = new Color(1f, 1f, 0f, 0.3f);
        Gizmos.DrawCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
        for(int i = 0; i < points.Length; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(points[i].transform.position, .3f);
        }
    }
}
