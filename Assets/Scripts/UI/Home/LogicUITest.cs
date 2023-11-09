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
        AnimationPopup.instance.AnimScaleZoom(panelSetting.transform.GetChild(0));
        
    }
    void ClosePanelSetting()
    {
        AnimationPopup.instance.AnimScaleZero(panelSetting, panelSetting.transform.GetChild(0));
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
        AnimationPopup.instance.AnimScaleZoom(selectBooster.transform.GetChild(0));
    }
    void ClosePanelBooster()
    {
        selectBooster.UnSelectedBtn();
        AnimationPopup.instance.AnimScaleZero(selectBooster.gameObject, selectBooster.transform.GetChild(0));

    }
    void OpenPanelShop()
    {
        panelHome.SetActive(false);
        imgHomeSelected.sprite = spriteSelects[1];

        panelShop.SetActive(true);
        imgShopSelected.sprite = spriteSelects[0];
    }

    void OpenPanelHome()
    {
        panelShop.SetActive(false);
        imgShopSelected.sprite = spriteSelects[1];

        panelHome.SetActive(true);
        imgHomeSelected.sprite = spriteSelects[0];
    }
}
