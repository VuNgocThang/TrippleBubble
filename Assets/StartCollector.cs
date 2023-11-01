using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCollector : MonoBehaviour
{
    public static StartCollector ins;
    public List<DataButton> listDataBtn = new List<DataButton>();
    public List<DataReward> listDataRw = new List<DataReward>();
    public List<ButtonSelector> listBtnSelector = new List<ButtonSelector>();
    public ButtonSelector prefab;
    public ListUnlockReward unlockReward = new ListUnlockReward();
    public int currentIndex;
    public int totalStar = 1200;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentIndex"))
        {
            currentIndex = 0;
            PlayerPrefs.SetInt("CurrentIndex", currentIndex);
            PlayerPrefs.Save();
        }
        else
        {
            currentIndex = PlayerPrefs.GetInt("CurrentIndex");
        }
        Debug.Log(currentIndex + " currentIndex");

        for (int i = 0; i < listDataBtn.Count; i++)
        {
            listDataBtn[i].id = i;
            listDataRw[i].id = i;

            ButtonSelector btn = Instantiate(prefab, transform);
            btn.Init(i, listDataBtn[i].cost, listDataRw[i].value, listDataRw[i].icon);

            DataUnlockReward data = new DataUnlockReward();
            unlockReward.listUnlockReward.Add(data);
            if (btn.id > currentIndex)
            {
                btn.btnBuy.interactable = false;
            }
            listBtnSelector.Add(btn);
        }
        UnlockNewBtnSelector();
        LoadDataItems();
    }

    void UnlockNewBtnSelector()
    {
        for (int i = 0; i < listBtnSelector.Count; i++)
        {
            int a = i;

            listBtnSelector[a].btnBuy.onClick.AddListener(() =>
            {
                Debug.Log(listBtnSelector[a].id);
                listBtnSelector[a].btnBuy.interactable = false;
                listBtnSelector[a].check.SetActive(true);
                currentIndex++;
                listBtnSelector[a].idBought = 1;
                SaveDataItemsJson(a);
                PlayerPrefs.SetInt("CurrentIndex", currentIndex);
                PlayerPrefs.Save();
                UpdateUnlockBtn();
            });
        }
    }

    void UpdateUnlockBtn()
    {
        foreach (ButtonSelector buttonSelector in listBtnSelector)
        {
            if (buttonSelector.id == currentIndex)
            {
                buttonSelector.btnBuy.interactable = true;
            }
        }
    }
    public void SaveDataItemsJson(int i)
    {
        unlockReward.listUnlockReward[i].id = listBtnSelector[i].idBought;
        //dataItemsJson.idUsing = idUsing;
        string json = JsonUtility.ToJson(unlockReward, true);
        PlayerPrefs.SetString("Data_Huy", json);
    }
    void LoadDataItemsJson()
    {
        for (int i = 0; i < listBtnSelector.Count; i++)
        {
            listBtnSelector[i].idBought = unlockReward.listUnlockReward[i].id;
            if (listBtnSelector[i].idBought == 1)
            {
                listBtnSelector[i].check.gameObject.SetActive(true);
            }
        }
    }
    public void LoadDataItems()
    {
        if (PlayerPrefs.GetString("Data_Huy").Equals(""))
        {
            SaveDataItemsJson(0);
            unlockReward = JsonUtility.FromJson<ListUnlockReward>(PlayerPrefs.GetString("Data_Huy"));
            LoadDataItemsJson();
        }
        else
        {
            unlockReward = JsonUtility.FromJson<ListUnlockReward>(PlayerPrefs.GetString("Data_Huy"));
            LoadDataItemsJson();
        }
    }

}
