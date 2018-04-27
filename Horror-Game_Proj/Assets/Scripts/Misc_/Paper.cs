using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
	public Image paper;
	public void ShowPaper()
	{
		paper.enabled = true;
		gameObject.SetActive(false);
		Invoke("HidePaper", 2);
	}

	void HidePaper()
	{
		paper.enabled = false;
		Destroy(gameObject);
	}
}
