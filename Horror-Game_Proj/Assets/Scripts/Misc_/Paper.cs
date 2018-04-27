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
        InvokeRepeating("HidePaper", 0, Time.deltaTime);
    }

    void HidePaper()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paper.enabled = false;
            Destroy(gameObject);
        }
    }
}
