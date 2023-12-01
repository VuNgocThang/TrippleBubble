using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class WinUI : MonoBehaviour
{
    public TextMeshProUGUI txtStar;
    public TextMeshProUGUI txtPoint;
    public TextMeshProUGUI txtPointMulti;

    public Transform hand;
    public Vector3 startPos;
    public Vector3 endPos;
    public Button btnStop;

    public GameObject pile;
    public List<GameObject> pileOfStars;
    public Vector3[] initPos;
    public Quaternion[] initRos;
    public Transform endPosStar;
    int currentScore;

    public int starAdd;

    private void OnGUI()
    {
        int star = 100 + (DataUseInGame.gameData.indexLevel + 1) * 20 + Mathf.RoundToInt(LogicGame.instance.timer.timeLeft) * 2;
        txtPoint.text = star.ToString();
        starAdd = star * MultiResult(hand.GetComponent<RectTransform>());
        txtPointMulti.text = starAdd.ToString();
    }

  
    private void Start()
    {
        currentScore = DataUseInGame.gameData.star;
        Move();
        InitPileCoin();
        btnStop.onClick.AddListener(StopMoveHand);
    }

    void Move()
    {
        hand.DOLocalMove(endPos, 1.5f)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                hand.DOLocalMove(startPos, 1.5f)
                .SetEase(Ease.InOutQuad)
                .OnComplete(Move);
            });
    }
    public void StopMoveHand()
    {
        DOTween.Kill(hand);
        btnStop.interactable = !btnStop.interactable;
        MultiResult(hand.GetComponent<RectTransform>());
        RewardPileOfCoin();
    }

    public int MultiResult(RectTransform hand)
    {
        int multi;
        float x = Mathf.Abs(hand.anchoredPosition.x);
        if (x < 22f)
        {
            multi = 5;
        }
        else if (x < 82f)
        {
            multi = 4;
        }
        else if (x < 147f)
        {
            multi = 3;
        }
        else
        {
            multi = 2;
        }

        return multi;
    }

    public int Multi()
    {
        return MultiResult(hand.GetComponent<RectTransform>());
    }

    void InitPileCoin()
    {
        for (int i = 0; i < pileOfStars.Count; i++)
        {
            initPos[i] = pileOfStars[i].transform.position;
            initRos[i] = pileOfStars[i].transform.rotation;
        }
    }
    private void Reset()
    {
        for (int i = 0; i < pileOfStars.Count; i++)
        {
            pileOfStars[i].transform.position = initPos[i];
            pileOfStars[i].transform.rotation = initRos[i];
        }
    }

    private void RewardPileOfCoin()
    {
        Reset();

        var delay = 0f;

        pile.SetActive(true);

        for (int i = 0; i < pileOfStars.Count; i++)
        {
            pileOfStars[i].transform
                .DOScale(1f, 0.3f)
                .SetDelay(delay)
                .SetEase(Ease.OutBack);

            if (endPosStar != null)
            {
                pileOfStars[i].GetComponent<RectTransform>()
                    .DOMove(endPosStar.position, 0.5f)
                    .SetDelay(delay + 0.2f)
                    .SetEase(Ease.OutBack);
            }

            pileOfStars[i].transform
                .DORotate(Vector3.zero, 0.5f)
                .SetDelay(delay + 0.2f)
                .SetEase(Ease.OutBack);

            pileOfStars[i].transform
                .DOScale(0f, 0.3f)
                .SetDelay(delay + 1f)
                .SetEase(Ease.OutBack);

            delay += 0.1f;
        }
        StartCoroutine(CountStars());
    }

    IEnumerator CountStars()
    {
        //int b = starAdd;
        //int a = (int)(starAdd * 0.02 / 3f);
        //while (b > 0)
        //{
        //    if (b <= a)
        //    {
        //        b = a;
        //    }

        //    currentScore += a;
        //    b -= a;

        //    txtStar.text = currentScore.ToString();
        //    yield return new WaitForSeconds(0.02f);
        //}

        yield return new WaitForSeconds(1.5f);
        txtStar.text = DataUseInGame.gameData.star.ToString();
    }
}
