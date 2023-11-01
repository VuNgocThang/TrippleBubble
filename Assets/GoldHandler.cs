using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldHandler : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI txtGold;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("NumGold"))
        {
            gold = 0;
            PlayerPrefs.SetInt("NumGold", gold);
            PlayerPrefs.Save();
        }
        else
        {
            gold = PlayerPrefs.GetInt("NumGold");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            gold += 1;
            PlayerPrefs.SetInt("NumGold", gold);
            PlayerPrefs.Save();
        }
    }

    private void OnGUI()
    {
        txtGold.text = gold.ToString();
    }
}
