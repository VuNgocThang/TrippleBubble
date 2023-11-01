using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicUITest : MonoBehaviour
{
    public Button btnTest;
    public GameObject test;

    private void Start()
    {
        btnTest.onClick.AddListener(OpenTest);
    }

    void OpenTest()
    {
        test.SetActive(true);
    }
}
