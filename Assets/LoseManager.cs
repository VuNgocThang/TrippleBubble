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
    }
    public void ContinueTimeUp()
    {
        Debug.Log("continue");
        panelTimeUp.SetActive(false);
        loseUI.SetActive(false);
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
    }
    public void ContinueOutOfMove()
    {
        panelOutOfMove.SetActive(false);
        loseUI.SetActive(false);
        LogicGame.instance.checkLose = false;
        LogicGame.instance.UndoTripple();
    }


    //Logic Persident
    public void OpenPanelPersident()
    {
        panelPersident.SetActive(true);
    }
    public void Retry()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("SceneGame");
    }
    public void BackHome()
    {
        SceneManager.LoadScene("SceneHome");
    }
}
