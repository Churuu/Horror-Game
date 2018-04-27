using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public string sceneToSwitch;
    public AnimationClip fadeAnimation;
    public Animator _anim;
    bool locked = true;


    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            locked = !locked;

        if (locked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;

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
