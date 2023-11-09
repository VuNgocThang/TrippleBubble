using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBoosterManager : MonoBehaviour
{
    public List<ButtonBooster> buttons = new List<ButtonBooster>();
    private void Start()
    {
        Init();
        OnClick(buttons[0], buttons[0].selected, buttons[0].isSelected);
        OnClick(buttons[1], buttons[1].selected, buttons[1].isSelected);
        OnClick(buttons[2], buttons[2].selected, buttons[2].isSelected);

    }

    void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].InitButton();
            buttons[i].SaveStateBooster(buttons[i].nameBooster, 0);
            UpdateNumBooster(buttons[i].count, buttons[i].btnPlus.gameObject, buttons[i].numBtn, buttons[i].txtNumBtn, buttons[i].btn);
        }
    }

    private void Update()
    {
        IncreaseTest();
    }

    void UpdateNumBooster(int num, GameObject plusObj, GameObject numTextObj, TextMeshProUGUI txtNum, Button btn)
    {
        if (num > 0)
        {
            numTextObj.gameObject.SetActive(true);
            btn.interactable = true;
            txtNum.text = num.ToString();
        }
        else
        {
            plusObj.gameObject.SetActive(true);
            btn.interactable = false;
        }
    }

    void OnClick(ButtonBooster buttonBooster, GameObject selectedObj, bool isSelected)
    {
        buttonBooster.btn.onClick.AddListener(() =>
        {
            Debug.Log(buttonBooster.btn.name);
            Debug.Log(buttonBooster.count);

            selectedObj.SetActive(true);
            buttonBooster.isSelected = true;
            Debug.Log(buttonBooster.nameBooster);

            buttonBooster.SaveStateBooster($"{buttonBooster.nameBooster}", 1);
        });
    }

    void IncreaseTest()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            buttons[0].count++;
            PlayerPrefs.SetInt("NumLightning", buttons[0].count);
            PlayerPrefs.Save();
            Init();
        }

        if (Input.GetKeyUp(KeyCode.B))
        {
            buttons[1].count++;
            PlayerPrefs.SetInt("NumTimer", buttons[1].count);
            PlayerPrefs.Save();
            Init();
        }

        if (Input.GetKeyUp(KeyCode.N))
        {
            buttons[2].count++;
            PlayerPrefs.SetInt("NumHint", buttons[2].count);
            PlayerPrefs.Save();
            Init();
        }
    }

   

}
