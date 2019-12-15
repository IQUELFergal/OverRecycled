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
    }

    public void AddScore(int n) //Ajouter un +n qui apparait et tombe en devenant transparent et le mettre rouge si n est négatif
    {
        score += n;
        UpdateText();
        
        if (score > PlayerPrefs.GetInt("HighScore1", 0))
        {
            PlayerPrefs.SetInt("HighScore1", score);
        }
    }

    void UpdateText()
    {
        scoreText.text = score.ToString();
        PlayerPrefs.SetInt("LastestScore1", score);
    }
}
