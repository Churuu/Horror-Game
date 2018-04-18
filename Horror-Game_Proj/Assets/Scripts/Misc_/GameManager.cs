using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public string sceneToSwitch;
    public AnimationClip fadeAnimation;
    public Animator _anim;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EndGame()
    {
        _anim.SetTrigger("Fade");
        Invoke("SwitchScene", fadeAnimation.length + 1);
    }

    private void SwitchScene()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(sceneToSwitch);
    }
}
