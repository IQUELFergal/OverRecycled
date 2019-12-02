using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{

    void Start()
    {
        FindObjectOfType<Settings>().GetResolution();
    }

    // Update is called once per frame
    void Update()
    {
        PauseMenu pause = FindObjectOfType<PauseMenu>();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenu.GameIsPaused || pause.optionsMenuUI.activeSelf || pause.choiceMenuUI.activeSelf)
            {
                pause.Resume();
            }
            else
            {
                pause.Pause();
            }
        }
    }

    IEnumerator LoadingScreen()
    {
        
        yield return new WaitForSeconds(5);
    }
}
