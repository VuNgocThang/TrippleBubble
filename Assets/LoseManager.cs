using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseManager : MonoBehaviour
{
    public GameObject loseUI;
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
        AnimationPopup.instance.AnimScaleZoom(panelTimeUp.transform);
    }
    public void ContinueTimeUp()
    {
        Debug.Log("continue");
        AnimationPopup.instance.AnimScaleZero(loseUI, panelTimeUp.transform);
        LogicGame.instance.checkLose = false;
        timer.timeOut = false;
        timer.stopTimer = false;
        timer.timeLeft += 60f;
        timer.OnGUI();
    }

    //Logic OutOfMove
    public void OpenPanelOutOfMove()
    {
        panelOutOfMove.SetActive(true);
        AnimationPopup.instance.AnimScaleZoom(panelOutOfMove.transform);
    }
    public void ContinueOutOfMove()
    {
        AnimationPopup.instance.AnimScaleZero(loseUI, panelOutOfMove.transform);
        LogicGame.instance.canClick = false;
        LogicGame.instance.checkLose = false;
        timer.timeOut = false;
        timer.stopTimer = false;
        LogicGame.instance.UndoAll();
    }

    //Logic Persident
    public void OpenPanelPersident()
    {
        AnimationPopup.instance.AnimScaleZero(null, panelOutOfMove.transform);
        panelPersident.SetActive(true);
        AnimationPopup.instance.AnimScaleZoom(panelPersident.transform);
    }
    public void Retry()
    {
        AnimationPopup.instance.AnimScaleZero(null, panelPersident.transform);
        StartCoroutine(WaitForLoadSceneGame("SceneGame"));
    }
    public void BackHome()
    {
        AnimationPopup.instance.AnimScaleZero(null, panelPersident.transform);
        StartCoroutine(WaitForLoadSceneGame("SceneHome"));

    }

    IEnumerator WaitForLoadSceneGame(string str)
    {
        yield return new WaitForSeconds(0.45f);
        SceneManager.LoadScene(str);
        DOTween.KillAll();
    }
}
