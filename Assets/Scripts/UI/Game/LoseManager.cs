using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoseManager : MonoBehaviour
{
    public GameObject loseUI;
    public GameObject bg;
    public Timer timer;

    [Header("Time Up")]
    public GameObject panelTimeUp;
    public Button btnContinueTimeUp;
    public Button btnGiveUpTimeUp;
    public CanvasGroup panelTimeUpCG;

    [Header("Out of Move")]
    public GameObject panelOutOfMove;
    public Button btnContinueOutOfMove;
    public Button btnGiveUpOutOfMove;
    public CanvasGroup panelOutOfMoveCG;


    [Header("Persident")]
    public GameObject panelPersident;
    public Button btnRetry;
    public Button btnHome;
    public CanvasGroup panelPersidentCG;

    private void Start()
    {
        btnContinueTimeUp.onClick.AddListener(ContinueTimeUp);
        btnGiveUpTimeUp.onClick.AddListener(OpenPanelPersident);

        btnContinueOutOfMove.onClick.AddListener(ContinueOutOfMove);
        btnGiveUpOutOfMove.onClick.AddListener(OpenPanelPersident);

        btnRetry.onClick.AddListener(Retry);
        btnHome.onClick.AddListener(BackHome);
    }

    // Logic TimeUp
    public void OpenPanelTimeUp()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.fail);
        panelTimeUp.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelTimeUpCG.gameObject, 0, 200, 0.5f);
        panelTimeUpCG.DOFade(1f, 0.5f);
    }
    public void ContinueTimeUp()
    {
        if (DataUseInGame.gameData.gold >= 200)
        {
            AnimationPopup.instance.FadeWhileMoveUp(panelTimeUpCG.gameObject, 0.5f);
            panelTimeUpCG.DOFade(0f, 0.5f)
                .OnComplete(() =>
                {
                    panelTimeUp.SetActive(false);
                    loseUI.gameObject.SetActive(false);
                });
            GameManager.Instance.SubGold(200);
            timer.timeLeft += 60f;
            LogicGame.instance.checkLose = false;
            timer.OnGUI();
            timer.timeOut = false;
            StartCoroutine(LogicGame.instance.CanClickAgain());
        }
        else
        {
            Debug.Log("Not Enough Gold");
        }
    }

    

    //Logic OutOfMove
    public void OpenPanelOutOfMove()
    {
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.fail);
        panelOutOfMove.SetActive(true);
        panelOutOfMoveCG.DOFade(1f, 0.5f);
        AnimationPopup.instance.DoTween_Button(panelOutOfMoveCG.gameObject, 0, 200, 0.55f);
    }
    public void ContinueOutOfMove()
    {
        if (DataUseInGame.gameData.gold >= 200)
        {
            AnimationPopup.instance.FadeWhileMoveUp(panelOutOfMoveCG.gameObject, 0.5f);
            panelOutOfMoveCG.DOFade(0f, 0.5f)
                .OnComplete(() =>
                {
                    panelOutOfMove.SetActive(false);
                    loseUI.gameObject.SetActive(false);

                });

            GameManager.Instance.SubGold(200);
            LogicGame.instance.canClick = false;
            LogicGame.instance.checkLose = false;
            timer.stopTimer = true;
            timer.timeOut = false;
            LogicGame.instance.UndoAll();
        }
        else
        {
            Debug.Log("Not Enough Gold");
        }
    }

    //Logic Persident
    public void OpenPanelPersident()
    {
        AnimationPopup.instance.FadeWhileMoveUp(panelOutOfMoveCG.gameObject, 0.5f);
        panelOutOfMoveCG.DOFade(0f, 0.5f)
            .OnComplete(() =>
            {
                panelOutOfMove.SetActive(false);
            });

        AnimationPopup.instance.FadeWhileMoveUp(panelTimeUpCG.gameObject, 0.5f);
        panelTimeUpCG.DOFade(0f, 0.5f)
               .OnComplete(() =>
               {
                   panelTimeUp.SetActive(false);
               });

        panelPersident.SetActive(true);
        AnimationPopup.instance.DoTween_Button(panelPersidentCG.gameObject, 0, 200, 0.5f);
        panelPersidentCG.DOFade(1f, 0.5f);
    }
    public void Retry()
    {
        GameManager.Instance.SubHeart();
        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f);

        bg.SetActive(false);
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneGame"));
    }
    public void BackHome()
    {
        // LogicGame.instance.SubHeart();
        GameManager.Instance.SubHeart();
        AnimationPopup.instance.FadeWhileMoveUp(panelPersidentCG.gameObject, 0.5f);
        panelPersidentCG.DOFade(0f, 0.5f);
        bg.SetActive(false);
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneHome"));
    }





    //void OpenPanelSetting()
    //{
    //    panelSetting.SetActive(true);
    //    AnimationPopup.instance.DoTween_Button(panelSettingCG.gameObject, 0, 200, 0.5f);
    //    panelSettingCG.DOFade(1f, 0.5f);

    //}
    //void ClosePanelSetting()
    //{
    //    AnimationPopup.instance.FadeWhileMoveUp(panelSettingCG.gameObject, 0.5f);
    //    panelSettingCG.DOFade(0f, 0.5f)
    //        .OnComplete(() =>
    //        {
    //            panelSetting.SetActive(false);
    //        });
    //}

}
