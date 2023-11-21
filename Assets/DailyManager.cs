using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class DailyManager : MonoBehaviour
{
    public Button btnPlayThisDay;
    public LogicUITest logicUI;

    private void Start()
    {
        btnPlayThisDay.onClick.AddListener(LoadSceneGame);
        //Debug.Log(DataUseInGame.gameData.dailyData.Count + " count");
        //for (int i = 0; i < DataUseInGame.gameData.dailyData.Count; i++)
        //{
        //    Debug.Log(DataUseInGame.gameData.dailyData[i].year);
        //    Debug.Log(DataUseInGame.gameData.dailyData[i].month);
        //    Debug.Log(DataUseInGame.gameData.dailyData[i].day);
        //}
    }
    void LoadSceneGame()
    {
        logicUI.SelectBooster();
    }


}
