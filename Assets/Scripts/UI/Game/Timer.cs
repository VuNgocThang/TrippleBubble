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
        yield return new WaitForSeconds(3f);
        stopTimer = false;
        timeOut = false;
        isFreeze = false;
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
                Debug.Log("you lose");
                LogicGame.instance.checkLose = true;
                LogicGame.instance.Lose();
                LogicGame.instance.logicUI.OpenLoseUI();
                LogicGame.instance.logicUI.loseUI.OpenPanelTimeUp();

            }
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