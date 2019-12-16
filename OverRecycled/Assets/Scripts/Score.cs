using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;

    void Start()
    {
        UpdateText();
        PlayerPrefs.SetInt("LatestScore1", score);
    }

    public void AddScore(int n) //Ajouter un +n qui apparait et tombe en devenant transparent et le mettre rouge si n est négatif
    {
        score += n;
        UpdateText();
        PlayerPrefs.SetInt("LatestScore1", score);

        if (score > PlayerPrefs.GetInt("HighScore1", 0))
        {
            PlayerPrefs.SetInt("HighScore1", score);
        }
    }

    void UpdateText()
    {
        scoreText.text = score.ToString();
    }
}
