using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    public Image paper;
    public void ShowPaper()
    {
        paper.gameObject.SetActive(true);
        gameObject.SetActive(false);
        InvokeRepeating("HidePaper", 0, Time.deltaTime);
    }

    void HidePaper()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paper.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
