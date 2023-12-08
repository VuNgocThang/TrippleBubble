using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
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

    public int currentIndexStarCollector;

    public List<int> listIndexDaily;
    public float currentRewardDaily;
    public float maxRewardDaily;
    public bool isDaily;
    public int indexDailyLV;
    public int year;
    public int month;
    public int day;
    public List<DailyData> dailyData;

    public bool isTutHintDone;
    public bool isTutOtherDone;

    public GameData()
    {
        indexLevel = 0;
        listIndex = new List<int>
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8,
        };
        heart = 5;
        star = 0;
        gold = 0;
        numHintItem = 99;
        numShuffleItem = 99;
        numUndoItem = 99;
        numTrippleUndoItem = 99;
        numFreezeTimeItem = 99;
        isHeartInfinity = false;
        timeHeartInfinity = 0;
        timeStarCollector = 86400f;

        currentIndexStarCollector = 0;

        listIndexDaily = new List<int>()
        {
            0, 1, 2, 3, 4, 5, 6, 7, 8,
            9, 10, 11, 12, 13, 14, 15, 16,17,
            18, 19, 20, 21, 22, 23, 24,25,26,
            72, 73, 74, 75, 76, 77, 78, 79, 80,
            81, 82, 83, 84, 85 ,86, 87, 88, 89,
            90, 91, 92, 93, 94, 95, 96, 97, 98,
            99, 100, 101, 102, 103, 104, 105, 106, 107
        };
        currentRewardDaily = 0;
        maxRewardDaily = 28;
        isDaily = false;
        indexDailyLV = -1;
        year = 2023;
        dailyData = new List<DailyData>();

        isTutHintDone = false;
        isTutOtherDone = false;
    }
}

[Serializable]
public class DailyData
{
    public int year;
    public int month;
    public int day;
}


public class DataUseInGame : MonoBehaviour
{
    public static DataUseInGame instance;
    public static GameData gameData;

    [HideInInspector] public float countdownTimerHeartInfinity;
    [HideInInspector] public float countdownTimerStarCollector;

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

        if (gameData.timeStarCollector <= 0)
        {
            gameData.timeStarCollector = 86400f;
            gameData.currentIndexStarCollector = 0;
            if (StartCollector.ins != null)
            {
                StartCollector.ins.currentIndex = 0;
                StartCollector.ins.UpdateUnlockBtn();
            }
            Debug.Log("Reset time");
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

        PlayerPrefs.SetFloat("CountdownTimerStarCollector", countdownTimerStarCollector);
        PlayerPrefs.SetString("LastTimerStarQuit", DateTime.Now.ToString());

        PlayerPrefs.Save();

        gameData.isDaily = false;
        SaveData();
    }
}