using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == "Player")
			FindObjectOfType<Ai>().currentFloorToPatrol = this.gameObject;
	}

	void OnDrawGizmos()
    {

        Gizmos.color = new Color(1, .5f, .5f, 1f);
        Gizmos.DrawWireCube(GetComponent<BoxCollider>().transform.localPosition, GetComponent<BoxCollider>().transform.localScale);

        Gizmos.color = new Color(1, .5f, .5f, 0.2f);
        Gizmos.DrawCube(GetComponent<BoxCollider>().transform.localPosition, GetComponent<BoxCollider>().transform.localScale);

    }
}
