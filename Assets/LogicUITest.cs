using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicUITest : MonoBehaviour
{
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnCloseSetting;
    [SerializeField] GameObject panelSetting;

    [SerializeField] Button btnStarCollector;
    [SerializeField] Button btnCloseStarCollector;
    [SerializeField] GameObject panelStarCollector;

    private void Start()
    {
        btnSetting.onClick.AddListener(OpenPanelSetting);
        btnCloseSetting.onClick.AddListener(ClosePanel);

        btnStarCollector.onClick.AddListener(OpenPanelStarCollector);
        btnCloseStarCollector.onClick.AddListener(ClosePanelStarCollector);
    }

    void OpenPanelSetting()
    {
        panelSetting.SetActive(true);
    }
    void ClosePanel()
    {
        panelSetting.SetActive(false);
    }

    void OpenPanelStarCollector()
    {
        panelStarCollector.SetActive(true);
    }
    void ClosePanelStarCollector()
    {
        panelStarCollector.SetActive(false);
    }


}
