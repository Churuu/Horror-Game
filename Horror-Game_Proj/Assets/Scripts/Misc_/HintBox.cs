using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintBox : MonoBehaviour
{

    public void Hint(string message)
    {
        GetComponent<Animator>().SetTrigger("Fade");
        GetComponent<Text>().text = message;
    }
}
