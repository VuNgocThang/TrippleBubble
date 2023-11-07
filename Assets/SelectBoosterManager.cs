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

    private void Start()
    {
        btnStart.onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        for (int i = 0; i < btnBoosters.Count; i++)
        {
            if (btnBoosters[i].isSelected)
            {
                btnBoosters[i].SubCount();
            }
        }
        AnimationPopup.instance.AnimScaleZero(this.gameObject, this.transform.GetChild(0));
        StartCoroutine(WaitForLoadSceneGame());
    }

    IEnumerator WaitForLoadSceneGame()
    {
        Debug.Log("load");
        yield return new WaitForSeconds(0.45f);
        SceneManager.LoadScene("SceneGame");
        DOTween.KillAll();
    }

    public void UnSelectedBtn()
    {
        for (int i = 0; i < btnBoosters.Count; i++)
        {
            btnBoosters[i].selected.SetActive(false);
        }
    }

}
