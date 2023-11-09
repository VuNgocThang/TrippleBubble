using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBooster : MonoBehaviour
{
    public string nameBooster;
    public string txt;
    public bool isSelected;
    public GameObject selected;
    public Button btn;
    public Button btnPlus;
    public GameObject numBtn;
    public TextMeshProUGUI txtNumBtn;
    public int count;

    public void InitButton()
    {
        if (!PlayerPrefs.HasKey($"{txt}"))
        {
            count = 0;
            btnPlus.gameObject.SetActive(true);
            PlayerPrefs.SetInt($"{txt}", count);
            PlayerPrefs.Save();
        }
        else
        {
            count = PlayerPrefs.GetInt($"{txt}");
        }
    }

    public void SubCount()
    {
        count--;
        PlayerPrefs.SetInt($"{txt}", count);
        PlayerPrefs.Save();
    }

    public void SaveStateBooster(string str, int i)
    {
        PlayerPrefs.SetInt(str, i);
        PlayerPrefs.Save();
    }
}
