using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public bool hasOpeningPassed = false;

    public Animator animator;

    void Start()
    {
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            FindObjectOfType<AudioManager>().Play("Bip");
            FadeToMainMenu();
        }
    }
    public void FadeToMainMenu()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }
}
