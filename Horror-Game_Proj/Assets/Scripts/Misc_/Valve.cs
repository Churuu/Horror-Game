using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valve : MonoBehaviour
{
	public int maxTurns;
	[HideInInspector] public int position = 0;


	public void TurnValve()
	{
		if (position == maxTurns)
		{
			position = 0;
		}else
		{
			position++;
		}
		FindObjectOfType<ValveHandler>().UpdateValve();
	}

}
