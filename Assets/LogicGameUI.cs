using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicGameUI : MonoBehaviour
{
    [SerializeField] Timer timer;

    [Header("Pause")]
    [SerializeField] Button btnPause;
    [SerializeField] Button btnResume;
    [SerializeField] Button btnClosePanelPause;
    [SerializeField] Button btnReplay;
    [SerializeField] GameObject panelPause;


    [Header("NoAds")]
    [SerializeField] Button btnRemoveAds;
    [SerializeField] Button btnClosePanelRemoveAds;
    [SerializeField] GameObject panelRemoveAds;

    [Header("WinUI")]
    public GameObject winUI;
    public LoseManager loseUI;
    private void Start()
    {
        btnPause.onClick.AddListener(OpenPanelPause);
        btnResume.onClick.AddListener(ClosePanelPause);
        btnClosePanelPause.onClick.AddListener(ClosePanelPause);
        btnReplay.onClick.AddListener(ReplayGame);

        btnRemoveAds.onClick.AddListener(OpenPanelRemoveAds);
        btnClosePanelRemoveAds.onClick.AddListener(ClosePanelRemoveAds);
    }
    public void OnWinUI()
    {
        winUI.SetActive(true);
    }

    void OpenPanelPause()
    {
        timer.stopTimer = true;
        panelPause.SetActive(true);
    }

    void ClosePanelPause()
    {
        timer.stopTimer = false;
        panelPause.SetActive(false);
    }
    void ReplayGame()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("SceneGame");
    }

    void OpenPanelRemoveAds()
    {
        timer.stopTimer = true;
        panelRemoveAds.SetActive(true);
    }
    void ClosePanelRemoveAds()
    {
        timer.stopTimer = false;
        panelRemoveAds.SetActive(false);
    }

    public void OpenLoseUI()
    {
        loseUI.gameObject.SetActive(true);
    }
}
