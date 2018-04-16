using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveHandler : MonoBehaviour
{

    public string combination;
    public GameObject[] valves;
    public string cur_combination = "000";

    public void UpdateValve()
    {
		cur_combination = "";
        for (int i = 0; i < valves.Length; i++)
        {
            cur_combination = cur_combination + valves[i].GetComponent<Valve>().position.ToString();
        }
    }

	void Update()
	{
		if (cur_combination == combination)
		{
			//Do Something
		}
	}
}
