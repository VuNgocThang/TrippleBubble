using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class HeartTest : MonoBehaviour
{
    public int heart;
    public int maxHeart = 5;
    public float countdownTimer;
    public TextMeshProUGUI txtNumHeart;
    public TextMeshProUGUI txtCountdownTimer;
    float minutes;
    float seconds;
    public bool canPlusHeart;
    int time = 1800;

    private DateTime lastHeartLossTime;

    private void Start()
    {
        canPlusHeart = true;

        if (!PlayerPrefs.HasKey("NumHeart"))
        {
            heart = 5;
            SaveHeart();
        }
        else
        {
            heart = PlayerPrefs.GetInt("NumHeart");
        }


        if (PlayerPrefs.HasKey("CountdownTimer"))
        {
            Debug.Log("date time. now" + DateTime.Now);
            Debug.Log("playerprefs" + DateTime.Parse(PlayerPrefs.GetString("LastHeartLossTime")));
            float timeSinceLastLoss = (float)(DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastHeartLossTime"))).TotalSeconds;

            int increaseHeart = (int)timeSinceLastLoss / time;
            float timeSub = timeSinceLastLoss % time;

            heart += increaseHeart;
            heart = Mathf.Min(heart, maxHeart);
            SaveHeart();
            countdownTimer = PlayerPrefs.GetFloat("CountdownTimer") - timeSub;
            countdownTimer = Mathf.Max(countdownTimer, 0);

            if (heart >= maxHeart)
            {
                countdownTimer = time;
            }
        }
        else
        {
            countdownTimer = time;
        }


        lastHeartLossTime = DateTime.Now;
        txtNumHeart.text = heart.ToString();
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat("CountdownTimer", countdownTimer);
        if (heart < maxHeart)
        {
            PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
        PlayerPrefs.Save();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            heart--;
            if (heart <= 0)
            {
                heart = 0;
            }

            SaveHeart();
        }

        if (countdownTimer <= 0 && canPlusHeart)
        {
            heart++;
            SaveHeart();
            lastHeartLossTime = DateTime.Now;
            countdownTimer = time;
            canPlusHeart = false;
        }
    }

    private void OnGUI()
    {
        txtNumHeart.text = heart.ToString();

        if (heart == maxHeart)
        {
            txtCountdownTimer.text = "FULL";
        }
        else
        {
            TimeSpan timeSinceLoss = DateTime.Now - lastHeartLossTime;

            if (timeSinceLoss.TotalSeconds < countdownTimer)
            {
                countdownTimer -= (float)timeSinceLoss.TotalSeconds;
            }
            else
            {
                countdownTimer = 0;
            }

            lastHeartLossTime = DateTime.Now;

            if (countdownTimer > 0)
            {
                canPlusHeart = true;
                minutes = Mathf.Floor(countdownTimer / 60);
                seconds = Mathf.RoundToInt(countdownTimer % 60);

                txtCountdownTimer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
            }
        }


    }

    private void OnApplicationQuit()
    {
        SaveHeart();
        PlayerPrefs.SetFloat("CountdownTimer", countdownTimer);
        PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    public void SaveHeart()
    {
        PlayerPrefs.SetInt("NumHeart", heart);
        PlayerPrefs.Save();
    }
}
