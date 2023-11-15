using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectBoosterManager : MonoBehaviour
{
    [SerializeField] Button btnStart;
    [SerializeField] ButtonBoosterManager btnBoosterManager;
    [SerializeField] List<ButtonBooster> btnBoosters;
    public CanvasGroup selectBoosterCG;

    private void Start()
    {
        btnStart.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        if (DataUseInGame.gameData.heart > 0 || DataUseInGame.gameData.isHeartInfinity)
        {
            for (int i = 0; i < btnBoosters.Count; i++)
            {
                if (btnBoosters[i].isSelected)
                {
                    btnBoosters[i].SubCount();
                }
            }
            AnimationPopup.instance.FadeWhileMoveUp(selectBoosterCG.gameObject, 0.5f);
            selectBoosterCG.DOFade(0f, 0.5f)
                .OnComplete(() =>
                {
                    selectBoosterCG.gameObject.SetActive(false);
                    SceneManager.LoadScene("SceneGame");
                    DOTween.KillAll();
                });
        }
        else
        {
            Debug.Log("Not Enough Heart");
        }


    }

    public void UnSelectedBtn()
    {
        for (int i = 0; i < btnBoosters.Count; i++)
        {
            btnBoosters[i].selected.SetActive(false);
            btnBoosters[i].SaveStateBooster(btnBoosters[i].nameBooster, 0);
        }
    }

}
