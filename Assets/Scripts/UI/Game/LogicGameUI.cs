using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

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

    [Header("Persident")]
    public Button btnClosePersident;
    public Button btnRetry;
    public Button btnHomePersident;
    public GameObject bgBlackPersident;
    public GameObject panelPersident;
    public CanvasGroup panelPersidentCG;

    [Header("NoAds")]
    [SerializeField] Button btnRemoveAds;
    [SerializeField] Button btnClosePanelRemoveAds;
    [SerializeField] GameObject panelRemoveAds;
    [SerializeField] CanvasGroup panelRemoveAdsCG;

    [Header("LoseManager")]
    public LoseManager loseUI;

    [Header("WinUI")]
    [SerializeField] WinUI winUI;
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

        btnClosePersident.onClick.AddListener(ClosePersident);
        btnRetry.onClick.AddListener(Retry);
        btnHomePersident.onClick.AddListener(BackHomePersident);

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
        OpenPanelPersident();
    }
    public void OpenPanelPersident()
    {
        bgBlackPersident.SetActive(true);
        btnClosePersident.gameObject.SetActive(true);
        LogicGame.instance.canClick = false;

        AnimationPopup.instance.FadeWhileMoveUp(panelPauseCG.gameObject, 0.5f);
        panelPauseCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelPause.SetActive(false);
            });

        panelPersident.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelPersidentCG.gameObject, 0, 200, 0.5f);
        panelPersidentCG.DOFade(1f, 0.5f);
    }
    public void ClosePersident()
    {
        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                bgBlackPersident.SetActive(false);
                panelPersident.SetActive(false);
                StartCoroutine(CanClickAgain());

            });
    }
    public void Retry()
    {
        GameManager.Instance.SubHeart();
        btnRetry.interactable = false;
        btnHome.interactable = false;
        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                btnRetry.interactable = true;
                btnHome.interactable = true;
            });

        bgBlackPersident.SetActive(false);
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneGame"));
    }
    public void BackHomePersident()
    {
        GameManager.Instance.SubHeart();
        btnRetry.interactable = false;
        btnHome.interactable = false;
        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                btnRetry.interactable = true;
                btnHome.interactable = true;
            });

        bgBlackPersident.SetActive(false);
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneHome"));
    }

    void OpenPanelRemoveAds()
    {
        timer.stopTimer = true;
        LogicGame.instance.canClick = false;
        panelRemoveAds.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelRemoveAdsCG.gameObject, 0, 200, 0.5f);
        panelRemoveAdsCG.DOFade(1f, 0.5f);
    }
    void ClosePanelRemoveAds()
    {
        AnimationPopup.instance.FadeWhileMoveUp(panelRemoveAdsCG.gameObject, 0.5f);
        panelRemoveAdsCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelRemoveAds.gameObject.SetActive(false);
                StartCoroutine(CanClickAgain());
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
        //GameManager.Instance.AddStar();
        GameManager.Instance.AddStar(winUI.Multi());
        AnimationPopup.instance.FadeWhileMoveUp(winUICG.gameObject, 0.5f);
        winUICG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                winUI.gameObject.SetActive(false);
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

                        SceneManager.LoadScene("SceneGame");
                    }
                }
                else
                {
                    DataUseInGame.gameData.isDaily = false;

                    //if (DataUseInGame.gameData.currentRewardDaily < DataUseInGame.gameData.maxRewardDaily)
                    //{
                    DataUseInGame.gameData.currentRewardDaily++;
                    //}

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

    IEnumerator CanClickAgain()
    {
        yield return new WaitForSeconds(0.2f);
        timer.stopTimer = false;
        LogicGame.instance.canClick = true;
    }
}
