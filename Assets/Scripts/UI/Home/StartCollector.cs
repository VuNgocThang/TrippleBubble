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
    public Transform parent;

    private void Awake()
    {
        ins = this;
    }
    private void Start()
    {
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
            btn.Init(listDataRw[i].id, listDataBtn[i].cost, listDataRw[i].value, listDataRw[i].icon, listDataRw[i].imgBtn);

            DataUnlockReward data = new DataUnlockReward();
            unlockReward.listUnlockReward.Add(data);
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
            if (btn.id== index)
            {
                return listDataRw.IndexOf(btn);
            }
        }
        return -1;
    }

    void UnlockNewBtnSelector()
    {
        for (int i = 0; i < listBtnSelector.Count; i++)
        {
            int a = i;

            listBtnSelector[a].btnBuy.onClick.AddListener(() =>
            {
                listBtnSelector[a].btnBuy.interactable = false;

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
        string json = JsonUtility.ToJson(unlockReward, true);
        PlayerPrefs.SetString("Data_Huy", json);
    }
    void LoadDataItemsJson()
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
