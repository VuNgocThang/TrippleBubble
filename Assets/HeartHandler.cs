using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HeartHandler : MonoBehaviour
{
    public int heart;
    public float countdownTimer;
    public TextMeshProUGUI txtNumHeart;
    public TextMeshProUGUI txtCountdownTimer;
    float minutes;
    float seconds;
    private void Start()
    {
        countdownTimer = 10;
        if (!PlayerPrefs.HasKey("NumHeart"))
        {
            heart = 0;
            PlayerPrefs.SetInt("NumHeart", heart);
            PlayerPrefs.Save();
        }
        else
        {
            heart = PlayerPrefs.GetInt("NumHeart");
        }
        txtNumHeart.text = heart.ToString();
    }
    private void Update()
    {
        UpdateTextCountDownTImer();
    }
    void UpdateTextCountDownTImer()
    {
        if (heart == 5)
        {
            txtCountdownTimer.text = "FULL";
        }
        countdownTimer -= Time.deltaTime;
    }

    private void OnGUI()
    {
        if (countdownTimer > 0)
        {
            minutes = Mathf.Floor(countdownTimer / 60);
            seconds = Mathf.RoundToInt(countdownTimer % 60);

            txtCountdownTimer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            countdownTimer = 0;
            //countdownTimer = 0;
            //heart += 1;
            //PlayerPrefs.SetInt("NumHeart", heart);
            //PlayerPrefs.Save();
            //txtNumHeart.text = heart.ToString();
        }
    }
}
