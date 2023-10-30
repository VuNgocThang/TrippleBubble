using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLeft;
    public float minutes;
    public float seconds;
    public bool timeOut;
    public bool stopTimer;
    public bool isFreeze;

    private void Start()
    {
        stopTimer = false;
        timeOut = false;
        isFreeze = false;
        timeLeft = 1000;
    }
    private void Update()
    {
        if (!stopTimer && !isFreeze)
        {
            timeLeft -= Time.deltaTime;
        }

        if (timeLeft <= 0 && !LogicGame.instance.checkWin)
        {
            timeOut = true;
            Debug.Log("you lose");
            LogicGame.instance.checkLose = true;
        }
    }

    private void OnGUI()
    {
        if (!timeOut)
        {
            if (timeLeft > 0)
            {
                minutes = Mathf.Floor(timeLeft / 60);
                seconds = Mathf.RoundToInt(timeLeft % 60);
                timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
            else
            {
                stopTimer = true;
            }
        }
    }

}