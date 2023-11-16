using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public float timeLeft;
    float minutes;
    float seconds;
    public bool timeOut;
    public bool stopTimer;
    public bool isFreeze;


    public IEnumerator InitTimerSetting()
    {
        if (PlayerPrefs.GetInt("BoosterHint") == 1 || PlayerPrefs.GetInt("BoosterTimer") == 1 || PlayerPrefs.GetInt("BoosterLightning") == 1)
        {
            yield return new WaitForSeconds(2f);
            stopTimer = false;
            timeOut = false;
            isFreeze = false;
        }
        else
        {
            stopTimer = false;
            timeOut = false;
            isFreeze = false;
        }

    }
    private void Update()
    {
        if (!stopTimer && !isFreeze)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0 && !LogicGame.instance.checkLose)
            {
                timeLeft = 0;
                stopTimer = true;
                timeOut = true;
                Debug.Log("you lose out of time");
                LogicGame.instance.checkLose = true;
                LogicGame.instance.Lose();
                LogicGame.instance.logicUI.OpenLoseUI();
                LogicGame.instance.logicUI.loseUI.OpenPanelTimeUp();

            }
        }

        if (timeLeft < 0.6f)
        {
            LogicGame.instance.canClick = false;
        }
    }
    public void OnGUI()
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