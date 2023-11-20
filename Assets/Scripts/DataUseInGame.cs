using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int indexLevel;
    public List<int> listIndex;
    public int heart;
    public int star;
    public int gold;
    public int numHintItem;
    public int numShuffleItem;
    public int numUndoItem;
    public int numTrippleUndoItem;
    public int numFreezeTimeItem;
    public bool isHeartInfinity;
    public float timeHeartInfinity;
    public float timeStarCollector;

    public GameData()
    {
        indexLevel = 0;
        listIndex = new List<int>
        {
            0,1,2,3,4,5,6,9,10,11,12,13,14,15,18,19,20,21,22,23,24
        };
        heart = 5;
        star = 1000000;
        gold = 1000000;
        numHintItem = 100;
        numShuffleItem = 100;
        numUndoItem = 100;
        numTrippleUndoItem = 100;
        numFreezeTimeItem = 100;
        isHeartInfinity = false;
        timeHeartInfinity = 0;
        timeStarCollector = 84600f;
    }
}


public class DataUseInGame : MonoBehaviour
{
    public static DataUseInGame instance;
    public static GameData gameData;

    public float countdownTimerHeartInfinity;
    public float countdownTimerStarCollector;

    private void Awake()
    {
        if (instance != null) Destroy(instance.gameObject);
        instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        LoadData();

        CheckTimeHeartInfinity();

        CheckTimeStarCollector();
    }

    public void SaveData()
    {
        string s = JsonUtility.ToJson(gameData);
        PlayerPrefs.SetString("gamedata", s);
    }

    public void LoadData()
    {
        string s = PlayerPrefs.GetString("gamedata", "");
        if (string.IsNullOrEmpty(s))
        {
            gameData = new GameData();
            return;
        }

        gameData = JsonUtility.FromJson<GameData>(s);

        if (gameData.timeHeartInfinity <= 0)
        {
            gameData.timeHeartInfinity = 0;
            gameData.isHeartInfinity = false;
        }
    }

    private void Update()
    {
        if (gameData.timeHeartInfinity > 0)
        {
            gameData.timeHeartInfinity -= Time.deltaTime;
            gameData.isHeartInfinity = true;
            countdownTimerHeartInfinity = gameData.timeHeartInfinity;
        }

        if (gameData.timeHeartInfinity <= 0)
        {
            gameData.timeHeartInfinity = 0;
            gameData.isHeartInfinity = false;
        }

        if (gameData.timeStarCollector > 0)
        {
            gameData.timeStarCollector -= Time.deltaTime;
            countdownTimerStarCollector = gameData.timeStarCollector;
        }
        else
        {
            gameData.timeStarCollector = 0;
        }
    }

    void CheckTimeHeartInfinity()
    {
        if (PlayerPrefs.HasKey("CountdownTimerHeartInfinity"))
        {
            float timeSinceLastLoss = (float)(DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastTimerQuit"))).TotalSeconds;


            gameData.timeHeartInfinity = PlayerPrefs.GetFloat("CountdownTimerHeartInfinity") - timeSinceLastLoss;

            gameData.timeHeartInfinity = Mathf.Max(gameData.timeHeartInfinity, 0);

            if (gameData.timeHeartInfinity <= 0)
            {
                countdownTimerHeartInfinity = 0;
            }
        }
        else
        {
            countdownTimerHeartInfinity = gameData.timeHeartInfinity;
        }
    }
    void CheckTimeStarCollector()
    {
        if (PlayerPrefs.HasKey("CountdownTimerStarCollector"))
        {
            float timeSinceLastLoss = (float)(DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastTimerStarQuit"))).TotalSeconds;


            gameData.timeStarCollector = PlayerPrefs.GetFloat("CountdownTimerStarCollector") - timeSinceLastLoss;

            gameData.timeStarCollector = Mathf.Max(gameData.timeStarCollector, 0);

            if (gameData.timeStarCollector <= 0)
            {
                countdownTimerStarCollector = 0;
            }
        }
        else
        {
            countdownTimerStarCollector = gameData.timeStarCollector;
        }
    }


    private void OnApplicationQuit()
    {
        SaveData();

        PlayerPrefs.SetFloat("CountdownTimerHeartInfinity", countdownTimerHeartInfinity);
        PlayerPrefs.SetString("LastTimerQuit", DateTime.Now.ToString());
        PlayerPrefs.Save();


        SaveData();

        PlayerPrefs.SetFloat("CountdownTimerStarCollector", countdownTimerHeartInfinity);
        PlayerPrefs.SetString("LastTimerStarQuit", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

    

}