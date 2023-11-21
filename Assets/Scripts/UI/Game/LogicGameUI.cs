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
    [SerializeField] GameObject newTile;
    [SerializeField] GameObject panel;
    [SerializeField] Canvas canvas;
    [SerializeField] Camera camerUI;
    [SerializeField] Button btnClaim;
    [SerializeField] CanvasGroup winUICG;
    public Button btnClaimStar;


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
        btnClaimStar.onClick.AddListener(ClaimStar);

    }

    void BackHome()
    {
        DataUseInGame.gameData.isDaily = false;
        DataUseInGame.instance.SaveData();

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
        AnimationPopup.instance.DoTween_Button(winUICG.gameObject, 0, 200, 0.5f);
        winUICG.DOFade(1f, 0.5f);
    }

    public void ClaimStar()
    {
        GameManager.Instance.AddStar();
        AnimationPopup.instance.FadeWhileMoveUp(winUICG.gameObject, 0.5f);
        winUICG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                winUI.SetActive(false);
                if (!DataUseInGame.gameData.isDaily)
                {
                    if (DataUseInGame.gameData.indexLevel == 4 || DataUseInGame.gameData.indexLevel == 9)
                    {
                        newTile.SetActive(true);
                        int index = DataUseInGame.gameData.indexLevel;
                        if (index < LogicGame.instance.listLevel.Count - 1)
                        {
                            index++;
                        }
                        DataUseInGame.gameData.indexLevel = index;
                        DataUseInGame.instance.SaveData();
                    }
                    else
                    {
                        int index = DataUseInGame.gameData.indexLevel;
                        if (index < LogicGame.instance.listLevel.Count - 1)
                        {
                            index++;
                        }
                        DataUseInGame.gameData.indexLevel = index;
                        DataUseInGame.instance.SaveData();

                        SceneManager.LoadScene("SceneHome");
                    }
                }
                else
                {
                    DataUseInGame.gameData.isDaily = false;
                    DataUseInGame.gameData.dailyData.Add(new DailyData()
                    {
                        year = 2023,
                        month = DataUseInGame.gameData.month,
                        day = DataUseInGame.gameData.day,
                    });
                    DataUseInGame.instance.SaveData();
                    SceneManager.LoadScene("SceneHome");
                }

            });
    }

    public void CloseWinUI()
    {
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        camerUI.gameObject.SetActive(false);
        panel.SetActive(false);
        winUI.gameObject.SetActive(false);
        SceneManager.LoadScene("SceneHome");
    }

}
