using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public Transform parent;
    public int star;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
        star = DataUseInGame.gameData.star;
        Init();
        UnlockNewBtnSelector();
        LoadDataItems();
    }

    private void Init()
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
        for (int i = 0; i < listDataRw.Count; i++)
        {
            ButtonSelector btn = Instantiate(prefab, parent);
            btn.Init(listDataRw[i].id, listDataBtn[i].cost, listDataRw[i].value, listDataRw[i].icon, listDataRw[i].imgBtn, listDataRw[i].nameRW);

            DataUnlockReward data = new DataUnlockReward();
            unlockReward.listUnlockReward.Add(data);
            if (star < btn.cost)
            {
                btn.btnBuy.interactable = false;
            }

            if (btn.id > currentIndex)
            {
                btn.btnBuy.interactable = false;
                btn.lockObject.SetActive(true);
            }
            listBtnSelector.Add(btn);
        }
    }

    int GetButtonByIndex(int index)
    {
        foreach (var btn in listDataRw)
        {
            if (btn.id == index)
            {
                return listDataRw.IndexOf(btn);
            }
        }
        return -1;
    }

    public void UnlockNewBtnSelector()
    {
        for (int i = 0; i < listBtnSelector.Count; i++)
        {
            int a = i;

            listBtnSelector[a].btnBuy.onClick.AddListener(() =>
            {
                if (DataUseInGame.gameData.star < listBtnSelector[a].cost) return;
                GameManager.Instance.SubStar(listBtnSelector[a].cost);
                listBtnSelector[a].btnBuy.interactable = false;
                SwitchAdd(listBtnSelector[a].stringName, listBtnSelector[a].value);
                currentIndex++;
                listBtnSelector[a].idBought = 1;
                SaveDataItemsJson(a);
                PlayerPrefs.SetInt("CurrentIndex", currentIndex);
                PlayerPrefs.Save();
                UpdateUnlockBtn();

                if (currentIndex >= listBtnSelector.Count) return;

                listBtnSelector[GetButtonByIndex(currentIndex)].lockObject.SetActive(false);
            });
        }
    }

    public void UpdateUnlockBtn()
    {
        foreach (ButtonSelector buttonSelector in listBtnSelector)
        {
            if (buttonSelector.id == currentIndex)
            {
                buttonSelector.btnBuy.interactable = true;
            }
            if (star < buttonSelector.cost)
            {
                buttonSelector.btnBuy.interactable = false;
            }
        }
    }
    public void ResetStarCollector()
    {
        for (int i = 1; i < listBtnSelector.Count; i++)
        {
            SaveDataItemsJson(0);
        }
    }
    public void SaveDataItemsJson(int i)
    {
        unlockReward.listUnlockReward[i].id = listBtnSelector[i].idBought;
        string json = JsonUtility.ToJson(unlockReward, true);
        PlayerPrefs.SetString("DataStarCollector", json);
    }
    public void LoadDataItemsJson()
    {
        for (int i = 0; i < listBtnSelector.Count; i++)
        {
            int a = i;
            listBtnSelector[a].idBought = unlockReward.listUnlockReward[a].id;
            if (listBtnSelector[a].idBought == 1)
            {
                listBtnSelector[a].btnBuy.interactable = false;
                listBtnSelector[a].lockObject.SetActive(false);
            }
            else
            {
                listBtnSelector[a].btnBuy.interactable = true;
                listBtnSelector[a].lockObject.SetActive(true);
            }
        }
    }

    public void LoadDataItems()
    {
        if (PlayerPrefs.GetString("DataStarCollector").Equals(""))
        {
            SaveDataItemsJson(0);
            unlockReward = JsonUtility.FromJson<ListUnlockReward>(PlayerPrefs.GetString("DataStarCollector"));
            LoadDataItemsJson();
        }
        else
        {
            unlockReward = JsonUtility.FromJson<ListUnlockReward>(PlayerPrefs.GetString("DataStarCollector"));
            LoadDataItemsJson();
        }
    }

    public void SwitchAdd(string str, int value)
    {
        switch (str)
        {
            case "gold":
                GameManager.Instance.AddGold(value);
                break;
            case "star":
                // do something;
                break;
            case "hint":
                // do something;
                break;
            case "lightning":
                //do something;
                break;
            default:
                break;
        }
    }

}
