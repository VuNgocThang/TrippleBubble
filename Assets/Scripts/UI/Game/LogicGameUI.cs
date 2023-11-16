using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicGameUI : MonoBehaviour
{
    [SerializeField] Timer timer;

    [Header("Pause")]
    [SerializeField] Button btnHome;
    [SerializeField] Button btnPause;
    [SerializeField] Button btnResume;
    [SerializeField] Button btnClosePanelPause;
    [SerializeField] Button btnReplay;
    [SerializeField] GameObject panelPause;
    [SerializeField] CanvasGroup panelPauseCG;


    [Header("NoAds")]
    [SerializeField] Button btnRemoveAds;
    [SerializeField] Button btnClosePanelRemoveAds;
    [SerializeField] GameObject panelRemoveAds;
    [SerializeField] CanvasGroup panelRemoveAdsCG;

    [Header("LoseManager")]
    public LoseManager loseUI;

    [Header("WinUI")]
    [SerializeField] GameObject winUI;
    [SerializeField] Canvas canvas;
    [SerializeField] Camera camerUI;
    [SerializeField] GameObject panel;
    [SerializeField] Button btnClaim;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        btnHome.onClick.AddListener(BackHome);
        btnPause.onClick.AddListener(OpenPanelPause);
        btnResume.onClick.AddListener(ClosePanelPause);
        btnClosePanelPause.onClick.AddListener(ClosePanelPause);
        btnReplay.onClick.AddListener(ReplayGame);

        btnRemoveAds.onClick.AddListener(OpenPanelRemoveAds);
        btnClosePanelRemoveAds.onClick.AddListener(ClosePanelRemoveAds);

        btnClaim.onClick.AddListener(CloseWinUI);
    }

    void BackHome()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("SceneHome");
    }
    void OpenPanelPause()
    {
        timer.stopTimer = true;
        panelPause.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelPauseCG.gameObject, 0, 200, 0.5f);
        panelPauseCG.DOFade(1f, 0.5f);
    }

    void ClosePanelPause()
    {
        timer.stopTimer = false;
        AnimationPopup.instance.FadeWhileMoveUp(panelPauseCG.gameObject, 0.5f);
        panelPauseCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelPause.SetActive(false);
            });
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
        AnimationPopup.instance.DoTween_Button(panelRemoveAdsCG.gameObject, 0, 200, 0.5f);
        panelRemoveAdsCG.DOFade(1f, 0.5f);
    }
    void ClosePanelRemoveAds()
    {
        timer.stopTimer = false;
        AnimationPopup.instance.FadeWhileMoveUp(panelRemoveAdsCG.gameObject, 0.5f);
        panelRemoveAdsCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelRemoveAds.gameObject.SetActive(false);
            });
    }

    public void OpenLoseUI()
    {
        loseUI.gameObject.SetActive(true);
    }

    public void OpenWinUI()
    {
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        camerUI.gameObject.SetActive(true);
        panel.SetActive(true);
        winUI.gameObject.SetActive(true);
    }

    public void CloseWinUI()
    {
        int index = DataUseInGame.gameData.indexLevel;
        if (index < LogicGame.instance.listLevel.Count - 1)
        {
            index++;
        }
        DataUseInGame.gameData.indexLevel = index;
        DataUseInGame.instance.SaveData();

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        camerUI.gameObject.SetActive(false);
        panel.SetActive(false);
        winUI.gameObject.SetActive(false);
        SceneManager.LoadScene("SceneHome");
    }


}
