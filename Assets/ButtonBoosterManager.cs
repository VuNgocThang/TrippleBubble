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
    }

    void Init()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].InitButton();
            UpdateNumBooster(buttons[i].count, buttons[i].btnPlus.gameObject, buttons[i].numBtn, buttons[i].txtNumBtn, buttons[i].btn);
            OnClick(buttons[i], buttons[i].selected, buttons[i].isSelected);
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
