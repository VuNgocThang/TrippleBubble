﻿using DG.Tweening;
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
    [SerializeField] TextMeshProUGUI txtShop;
    [SerializeField] RectTransform imgShop;
    [SerializeField] GameObject panelShop;
    [SerializeField] Image imgShopSelected;
    [SerializeField] GameObject imgBottom;

    [Header("Home")]
    [SerializeField] Button btnHome;
    [SerializeField] TextMeshProUGUI txtHome;
    [SerializeField] RectTransform imgHome;
    [SerializeField] GameObject panelHome;
    [SerializeField] Image imgHomeSelected;
    [SerializeField] GameObject topObject;

    [Header("Daily")]
    [SerializeField] Button btnDaily;
    [SerializeField] TextMeshProUGUI txtDaily;
    [SerializeField] RectTransform imgDaily;
    [SerializeField] GameObject panelDaily;
    [SerializeField] Image imgDailySelected;


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
        btnDaily.onClick.AddListener(OpenDailyPanel);
    }

    void OpenPanelSetting()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelSetting.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelSettingCG.gameObject, 0, 200, 0.5f);
        panelSettingCG.DOFade(1f, 0.5f);

    }
    void ClosePanelSetting()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        AnimationPopup.instance.FadeWhileMoveUp(panelSettingCG.gameObject, 0.5f);
        panelSettingCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelSetting.SetActive(false);
            });
    }

    void OpenPanelStarCollector()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelStarCollector.SetActive(true);
    }
    void ClosePanelStarCollector()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelStarCollector.SetActive(false);
    }

    public void SelectBooster()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        selectBooster.gameObject.SetActive(true);
        AnimationPopup.instance.DoTween_Button(selectBooster.selectBoosterCG.gameObject, 0, 200, 0.5f);
        selectBooster.selectBoosterCG.DOFade(1f, 0.5f);

    }
    void ClosePanelBooster()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        AnimationPopup.instance.FadeWhileMoveUp(selectBooster.selectBoosterCG.gameObject, 0.5f);
        selectBooster.selectBoosterCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                selectBooster.gameObject.SetActive(false);
            });
    }

    // 1 unselect
    // 0 select
    void OpenPanelShop()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelHome.SetActive(false);
        imgHomeSelected.sprite = spriteSelects[1];
        txtHome.gameObject.SetActive(false);
        imgHome.DOAnchorPosY(25, 0.3f, true);
        topObject.SetActive(false);

        panelDaily.SetActive(false);
        imgDailySelected.sprite = spriteSelects[1];
        txtDaily.gameObject.SetActive(false);
        imgDaily.DOAnchorPosY(0, 0.3f, true);
        topObject.SetActive(false);

        panelShop.SetActive(true);
        imgBottom.SetActive(true);
        imgShopSelected.sprite = spriteSelects[0];
        imgShop.DOAnchorPosY(70, 0.3f, true).OnComplete(() =>
        {
            imgShop.gameObject.SetActive(true);
            txtShop.gameObject.SetActive(true);
        });

        DataUseInGame.gameData.isDaily = false;
        DataUseInGame.instance.SaveData();

    }

    void OpenPanelHome()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelShop.SetActive(false);
        imgBottom.SetActive(false);
        imgShopSelected.sprite = spriteSelects[1];
        txtShop.gameObject.SetActive(false);
        imgShop.DOAnchorPosY(0, 0.3f, true);

        panelDaily.SetActive(false);
        imgDailySelected.sprite = spriteSelects[1];
        txtDaily.gameObject.SetActive(false);
        imgDaily.DOAnchorPosY(0, 0.3f, true);

        topObject.SetActive(true);
        panelHome.SetActive(true);
        imgHomeSelected.sprite = spriteSelects[0];
        imgHome.DOAnchorPosY(87, 0.3f, true).OnComplete(() =>
        {
            imgHome.gameObject.SetActive(true);
            txtHome.gameObject.SetActive(true);
        });

        DataUseInGame.gameData.isDaily = false;
        DataUseInGame.instance.SaveData();

    }

    void OpenDailyPanel()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.clickMenu);
        panelHome.SetActive(false);
        imgBottom.SetActive(false);
        imgHomeSelected.sprite = spriteSelects[1];
        txtHome.gameObject.SetActive(false);
        imgHome.DOAnchorPosY(25, 0.3f, true);
        topObject.SetActive(true);

        panelShop.SetActive(false);
        imgShopSelected.sprite = spriteSelects[1];
        txtShop.gameObject.SetActive(false);
        imgShop.DOAnchorPosY(0, 0.3f, true);

        panelDaily.SetActive(true);
        imgDailySelected.sprite = spriteSelects[0];
        imgDaily.DOAnchorPosY(70, 0.3f, true).OnComplete(() =>
        {
            imgDaily.gameObject.SetActive(true);
            txtDaily.gameObject.SetActive(true);
        });

        DataUseInGame.gameData.isDaily = true;
        DataUseInGame.instance.SaveData();
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
