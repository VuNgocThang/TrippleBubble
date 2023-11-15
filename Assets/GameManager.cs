using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public void SubHeart()
    {
        int heart = DataUseInGame.gameData.heart;

        if (heart >= 5)
        {
            PlayerPrefs.SetString("LastHeartLossTime", DateTime.Now.ToString());
            PlayerPrefs.Save();
        }
        if (!DataUseInGame.gameData.isHeartInfinity)
        {
            heart--;
        }
        Debug.Log(heart);

        if (heart <= 0)
        {
            heart = 0;
        }

        DataUseInGame.gameData.heart = heart;
        DataUseInGame.instance.SaveData();
    }
    public void AddStar()
    {
        //100 + số level * 20 + số giây còn lại * 2
        int star = DataUseInGame.gameData.star;
        star += 100 + (DataUseInGame.gameData.indexLevel + 1) * 20 + Mathf.RoundToInt(LogicGame.instance.timer.timeLeft) * 2;
        DataUseInGame.gameData.star = star;
        DataUseInGame.instance.SaveData();
    }
    public void SubStar(int starSub)
    {
        int star = DataUseInGame.gameData.star;
        star -= starSub;
        DataUseInGame.gameData.star = star;
        DataUseInGame.instance.SaveData();
    }

    public void AddGold(int goldAdd)
    {
        Debug.Log(DataUseInGame.gameData.gold);
        int gold = DataUseInGame.gameData.gold;
        gold += goldAdd;
        DataUseInGame.gameData.gold = gold;
        DataUseInGame.instance.SaveData();
    }
    public void SubGold(int goldSub)
    {
        int gold = DataUseInGame.gameData.gold;
        gold -= goldSub;
        DataUseInGame.gameData.gold = gold;
        DataUseInGame.instance.SaveData();
    }
    public void AddItemHint(int itemHint)
    {
        int hint = DataUseInGame.gameData.numHintItem;
        hint += itemHint;
        DataUseInGame.gameData.gold = hint;
        DataUseInGame.instance.SaveData();
    }
}
