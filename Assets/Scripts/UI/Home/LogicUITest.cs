using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LogicUITest : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Button btnSetting;
    [SerializeField] Button btnCloseSetting;
    [SerializeField] GameObject panelSetting;
    [SerializeField] CanvasGroup panelSettingCG;

    [Header("Star Collector")]
    [SerializeField] Button btnStarCollector;
    [SerializeField] Button btnCloseStarCollector;
    [SerializeField] GameObject panelStarCollector;
    [SerializeField] TextMeshProUGUI txtTimerStarCollector;

    [Header("Play")]
    [SerializeField] Button btnPlay;
    [SerializeField] Button btnCloseSelectBooster;
    [SerializeField] SelectBoosterManager selectBooster;
    [SerializeField] CanvasGroup selectBoosterCG;


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
        AnimationPopup.instance.DoTween_Button(panelSettingCG.gameObject, 0, 200, 0.5f);
        panelSettingCG.DOFade(1f, 0.5f);

    }
    void ClosePanelSetting()
    {
        AnimationPopup.instance.FadeWhileMoveUp(panelSettingCG.gameObject, 0.5f);
        panelSettingCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelSetting.SetActive(false);
            });
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
        AnimationPopup.instance.DoTween_Button(selectBooster.selectBoosterCG.gameObject, 0, 200, 0.5f);
        selectBooster.selectBoosterCG.DOFade(1f, 0.5f);

    }
    void ClosePanelBooster()
    {
        AnimationPopup.instance.FadeWhileMoveUp(selectBooster.selectBoosterCG.gameObject, 0.5f);
        selectBooster.selectBoosterCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                selectBooster.gameObject.SetActive(false);
            });
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

    private void OnGUI()
    {
        float timerStarCollector = DataUseInGame.gameData.timeStarCollector;
        float hours = Mathf.Floor(timerStarCollector / 3600);

        float timePerHour = timerStarCollector - hours * 3600;
        float minutes = Mathf.Floor(timePerHour / 60);
        float seconds = Mathf.RoundToInt(timePerHour % 60);

        txtTimerStarCollector.text = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
