using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public string sceneToSwitch;
    public AnimationClip fadeAnimation;
    public Animator _anim;
    public bool mouseLocked = true;


    void Update()
    {
        if (mouseLocked)
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

    public void UnlockMouse()
    {
        mouseLocked = false;
    }

    public void LockMouse()
    {
        mouseLocked = true;
    }

    public void EndGame()
    {
        _anim.SetTrigger("Fade");
        Invoke("SwitchScene", fadeAnimation.length + 1);
        mouseLocked = false;
    }

    private void SwitchScene()
    {
        mouseLocked = false;
        SceneManager.LoadScene(sceneToSwitch);
    }
}
