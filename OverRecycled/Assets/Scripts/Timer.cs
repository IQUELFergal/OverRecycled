using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeLeft = 60.0f;
    public Text timerText;


    void Update()
    {

        if ((timerText != null) && (timeLeft >= Time.deltaTime))
        {
            timeLeft -= Time.deltaTime;
            System.TimeSpan result = System.TimeSpan.FromSeconds(timeLeft);
            System.DateTime actualResult = System.DateTime.MinValue.Add(result);
            if (timeLeft <= 60f)
            {
                timerText.text = actualResult.ToString("ss");
                if (timeLeft <= 10f)
                {
                    timerText.transform.localScale = new Vector3(1 + Mathf.PingPong(Time.time, 0.5f), 1 + Mathf.PingPong(Time.time, 0.5f), 0);
                    timerText.color = new Color(1, Mathf.PingPong(2 * Time.time, 1), Mathf.PingPong(2 * Time.time, 1));
                    FindObjectOfType<AudioManager>().SetVolume(Mathf.PingPong(2*Time.time, 1f));
                }
            }
            else timerText.text = actualResult.ToString("mm:ss");

        }
        else
        {
            GameOver();
        }

    }

    void GameOver() //Script executé à la fin du timer
    {
        Debug.Log("Game Over");
    }

}