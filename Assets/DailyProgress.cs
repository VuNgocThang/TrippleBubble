using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyProgress : MonoBehaviour
{
    public int currentReward;
    public int reward;
    public int max;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("MaxRewardDaily"))
        {
            max = 4;
            PlayerPrefs.SetInt("MaxRewardDaily", max);
            PlayerPrefs.Save();
        }
        else
        {
            max = PlayerPrefs.GetInt("MaxRewardDaily");
        }

        if (!PlayerPrefs.HasKey("RewardDaily"))
        {
            reward = 0;
            PlayerPrefs.SetInt("RewardDaily", reward);
            PlayerPrefs.Save();
        }
        else
        {
            reward = PlayerPrefs.GetInt("RewardDaily");
        }


        reward = PlayerPrefs.GetInt("RewardDaily");
        reward += 1;
        currentReward = reward;

        PlayerPrefs.SetInt("RewardTile", reward);
        PlayerPrefs.Save();

        if (currentReward > max) currentReward = max;
    }

    public void IncreaseMax()
    {
        if (reward >= max)
        {
            max += 1;
            Debug.Log(max);
            PlayerPrefs.SetInt("MaxRewardDaily", max);

            reward = 0;

            PlayerPrefs.SetInt("RewardDaily", reward);
            PlayerPrefs.Save();
        }
    }

}
