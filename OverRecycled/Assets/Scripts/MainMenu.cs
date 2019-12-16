using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Text highScore;
    public Text latestScore;

    public Button[] button;
    public GameObject[] buttonLevels;

    public Button playButton;

    public Image[] locks;

    public Text[] levelInGrey;

    public Toggle muteToggle;

    int unlockCondition = 10;

    public void Quit()
    {
        Application.Quit();
    }

    public void Play(int i)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + i);

        //FindObjectOfType<AudioManager>().Play(i.ToString());     BEUG : LANCE LES 2 SONS EN MEME TEMPS

        FindObjectOfType<AudioManager>().Play(PlayerPrefs.GetInt("LevelIndice", 0).ToString());
        FindObjectOfType<AudioManager>().Stop("MainTheme");
    }

    void Start()
    {
        //test
        //PlayerPrefs.SetInt("HighScore1", 0);

        for (int i=0; i<=3; i++)
        {
            if (PlayerPrefs.GetInt("HighScore" + (i+1).ToString(), 0) >= unlockCondition)   
            {
                buttonLevels[i].SetActive(true);
                locks[i].enabled = false;
            }
            else
            {
                buttonLevels[i].SetActive(false);
                locks[i].enabled = true;
                levelInGrey[i].color = new Color(0.5f, 0.5f, 0.5f, 1);
            }
        }

        //audio
        FindObjectOfType<AudioManager>().Play("MainTheme");

        FindObjectOfType<Settings>().GetResolution();
                
        button[0].onClick.AddListener(() => SetLevelIndice(1));   
        button[1].onClick.AddListener(() => SetLevelIndice(2));

        playButton.onClick.AddListener(() => Play(PlayerPrefs.GetInt("LevelIndice",0)));

        muteToggle.isOn = false;
    }

    public void SetLevelIndice(int i)
    {
        PlayerPrefs.SetInt("LevelIndice", i);
        highScore.text = PlayerPrefs.GetInt("HighScore"+i.ToString(), 0).ToString();
        latestScore.text = PlayerPrefs.GetInt("LatestScore"+i.ToString(), 0).ToString();
    }
}
