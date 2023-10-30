using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicGameUI : MonoBehaviour
{
    public GameObject winUI;

    public void OnWinUI()
    {
        winUI.SetActive(true);
    }
}
