using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    public string sceneToSwitch;
    Animator _anim;
    public AnimationClip fadeAnimation;

    void Start()
    {
		_anim = GetComponent<Animator>();
    }

    public void WonGame()
    {
		_anim.SetTrigger("Fade");
        Invoke("SwitchScene", fadeAnimation.length + 1);
    }

    private void SwitchScene()
    {
		FindObjectOfType<PlayerController>().UnlockCursor();
        SceneManager.LoadScene(sceneToSwitch);
    }
}
