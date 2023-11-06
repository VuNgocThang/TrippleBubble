using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicUITest : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnCloseSetting;
    [SerializeField] GameObject panelSetting;

    [Header("Star Collector")]
    [SerializeField] Button btnStarCollector;
    [SerializeField] Button btnCloseStarCollector;
    [SerializeField] GameObject panelStarCollector;

    [Header("Play")]
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnCloseSelectBooster;
    [SerializeField] SelectBoosterManager selectBooster;


    [Header("Shop")]
    [SerializeField] Button btnShop;
    [SerializeField] GameObject panelShop;
    [SerializeField] Image imgShopSelected;

    [Header("Home")]
    [SerializeField] Button btnHome;
    [SerializeField] GameObject panelHome;
    [SerializeField] Image imgHomeSelected;

    public List<Sprite> spriteSelects = new List<Sprite>();
    private void Start()
    {
        btnSetting.onClick.AddListener(OpenPanelSetting);
        btnCloseSetting.onClick.AddListener(ClosePanelSetting);

        btnStarCollector.onClick.AddListener(OpenPanelStarCollector);
        btnCloseStarCollector.onClick.AddListener(ClosePanelStarCollector);

        btnPlay.onClick.AddListener(SelectBooster);
        btnCloseSelectBooster.onClick.AddListener(ClosePanelBooster);

        btnShop.onClick.AddListener(OpenPanelShop);
        btnHome.onClick.AddListener(OpenPanelHome);
    }

    void OpenPanelSetting()
    {
        panelSetting.SetActive(true);
    }
    void ClosePanelSetting()
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

    void SelectBooster()
    {
        selectBooster.gameObject.SetActive(true);
    }
    void ClosePanelBooster()
    {
        selectBooster.UnSelectedBtn();
        selectBooster.gameObject.SetActive(false);
    }
    void OpenPanelShop()
    {
        panelHome.SetActive(false);
        //btnHome.GetComponent<Image>().sprite = spriteSelects[1];
        imgHomeSelected.sprite = spriteSelects[1];

        panelShop.SetActive(true);
        //btnShop.GetComponent<Image>().sprite = spriteSelects[0];
        imgShopSelected.sprite = spriteSelects[0];
    }

    void OpenPanelHome()
    {
        panelShop.SetActive(false);
        imgShopSelected.sprite = spriteSelects[1];
        //btnShop.GetComponent<Image>().sprite = spriteSelects[1];

        panelHome.SetActive(true);
        imgHomeSelected.sprite = spriteSelects[0];
        //btnHome.GetComponent<Image>().sprite = spriteSelects[0];
    }
}
