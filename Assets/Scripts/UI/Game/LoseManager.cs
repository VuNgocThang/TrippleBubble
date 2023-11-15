using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [Header("Out of Move")]
    public GameObject panelOutOfMove;
    public Button btnContinueOutOfMove;
    public Button btnGiveUpOutOfMove;

    [Header("Persident")]
    public GameObject panelPersident;
    public Button btnRetry;
    public Button btnHome;

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
        panelTimeUp.SetActive(true);
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.fail);
        AnimationPopup.instance.AnimScaleZoom(panelTimeUp.transform);
    }
    public void ContinueTimeUp()
    {
        //if (DataUseInGame.gameData.gold >= 200)
        //{
            AnimationPopup.instance.AnimScaleZero(loseUI, panelTimeUp.transform);
           // GameManager.Instance.SubGold(200);
            LogicGame.instance.checkLose = false;
            timer.timeOut = false;
            timer.stopTimer = false;
            timer.timeLeft += 60f;
            timer.OnGUI();
        //}
        //else
        //{
        //    Debug.Log("Not Enough Gold");
        //}

    }

    //Logic OutOfMove
    public void OpenPanelOutOfMove()
    {
        panelOutOfMove.SetActive(true);
        AudioManager.instance.UpdateSoundAndMusic(AudioManager.instance.aus, AudioManager.instance.fail);
        AnimationPopup.instance.AnimScaleZoom(panelOutOfMove.transform);
    }
    public void ContinueOutOfMove()
    {
        //if (DataUseInGame.gameData.gold >= 200)
        //{
            AnimationPopup.instance.AnimScaleZero(loseUI, panelOutOfMove.transform);
            //GameManager.Instance.SubGold(200);
            LogicGame.instance.canClick = false;
            LogicGame.instance.checkLose = false;
            timer.timeOut = false;
            timer.stopTimer = false;
            LogicGame.instance.UndoAll();
        //}
        //else
        //{
        //    Debug.Log("Not Enough Gold");
        //}
    }

    //Logic Persident
    public void OpenPanelPersident()
    {
        AnimationPopup.instance.AnimScaleZero(panelOutOfMove, panelOutOfMove.transform);
        AnimationPopup.instance.AnimScaleZero(panelTimeUp, panelOutOfMove.transform);
        panelPersident.SetActive(true);
        AnimationPopup.instance.AnimScaleZoom(panelPersident.transform);
    }
    public void Retry()
    {
        //LogicGame.instance.SubHeart();
        GameManager.Instance.SubHeart();
        AnimationPopup.instance.AnimScaleZero(null, panelPersident.transform);
        bg.SetActive(false);
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneGame"));
    }
    public void BackHome()
    {
        // LogicGame.instance.SubHeart();
        GameManager.Instance.SubHeart();
        AnimationPopup.instance.AnimScaleZero(null, panelPersident.transform);
        bg.SetActive(false);
        StartCoroutine(LogicGame.instance.AnimBoomBB("SceneHome"));
    }

}
