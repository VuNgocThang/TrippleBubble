using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class Rewardbar : MonoBehaviour
{
    public Image cooldown;
    float reward;
    public float maxReward;
    float count;
    public static Rewardbar ins;
    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        reward = PlayerPrefs.GetFloat("Reward");
        //IncreaseReward(reward, reward + 1);
        count = reward;
        reward += 1;
        PlayerPrefs.SetFloat("Reward", reward);
        PlayerPrefs.Save();
    }
    /* public void IncreaseReward(float value, float newValue)
     {
         DOTween.To(() => value, x => value = x, newValue, 2f).OnUpdate(() =>
         {
             cooldown.fillAmount = value / maxReward;
         }).SetEase(Ease.Linear);
     }*/
    private void Update()
    {
        LoadReward();
    }
    void LoadReward()
    {
        if (count < reward)
        {
            count += Time.deltaTime;
            cooldown.fillAmount = count / maxReward;
        }
    }
    int golds;
    public void getReward()
    {
        if (PlayerPrefs.GetFloat("Reward") == maxReward - 1)
        {
            PlayerPrefs.GetInt("Gold");
            PlayerPrefs.SetInt("Gold", golds + 10);
        }
        if (PlayerPrefs.GetFloat("Reward") == maxReward)
        {
            PlayerPrefs.SetFloat("Reward", reward);
            PlayerPrefs.Save();
        }
    }
}