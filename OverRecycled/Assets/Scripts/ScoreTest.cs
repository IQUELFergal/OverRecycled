using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTest : MonoBehaviour
{
    public ScoreManager[] scores;

    public Text score1;
    public Text score2;
    public Text highScore1;
    public Text highScore2;

    int number1 = 0;
    //int number2 = 0;

    void Start()
    {
        highScore1.text = PlayerPrefs.GetInt("HighScore1", 0).ToString();
        //highScore2.text = PlayerPrefs.GetInt("HighScore2", 0).ToString();

        score1.text = number1.ToString();

        PlayerPrefs.SetInt("CurrentScore1", number1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            number1 += 1;
            PlayerPrefs.SetInt("CurrentScore1", number1);
            Debug.Log("Current score1 : " + PlayerPrefs.GetInt("CurrentScore1", 0));

            score1.text = number1.ToString();

            if (number1 > PlayerPrefs.GetInt("HighScore1",0))
            {
                PlayerPrefs.SetInt("HighScore1", number1);
                highScore1.text = number1.ToString();
            } 
        }

/*        if (Input.GetKeyDown(KeyCode.S))
        {
            number2 += 1;
            score2.text = number2.ToString();

            if (number2 > PlayerPrefs.GetInt("HighScore2", 0))
            {
                PlayerPrefs.SetInt("HighScore2", number2);
                highScore2.text = number2.ToString();
            }
        }*/

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reset();
        }
    }

    public void Reset()
    {
        PlayerPrefs.DeleteAll();            //or DeleteKey("");
        highScore1.text = "0";
        //highScore2.text = "0";
    }
}
