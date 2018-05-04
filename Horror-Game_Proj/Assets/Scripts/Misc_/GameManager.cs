using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public string sceneToSwitch;
    public AnimationClip fadeAnimation;
    public Animator _anim;
    public bool locked = true;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            locked = !locked;

        if (locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

    public void EndGame()
    {
        _anim.SetTrigger("Fade");
        Invoke("SwitchScene", fadeAnimation.length + 1);
    }

    private void SwitchScene()
    {
        locked = false;
        SceneManager.LoadScene(sceneToSwitch);
    }
}
