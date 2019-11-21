using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Canvas endGameUI;

    void Update()
    {
        if (PlayerPrefs.GetInt("HighScore1", 0) >= 15)
        {
            endGameUI.gameObject.SetActive(true);
        }
        else
        {
            endGameUI.gameObject.SetActive(false);
        }
    }
}
