using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class WinUI : MonoBehaviour
{
    public TextMeshProUGUI txtPoint;
    public TextMeshProUGUI txtPointMulti;
    public GameObject vfx;

    public Transform hand;
    public Vector3 startPos;
    public Vector3 endPos;
    public Button btnStop;

    private void OnGUI()
    {
        int star = 100 + (DataUseInGame.gameData.indexLevel + 1) * 20 + Mathf.RoundToInt(LogicGame.instance.timer.timeLeft) * 2;
        txtPoint.text = star.ToString();
        txtPointMulti.text = (star * MultiResult(hand.GetComponent<RectTransform>())).ToString();
    }

    private void Update()
    {
        vfx.transform.Rotate(new Vector3(0, 0, 1) * 100f * Time.deltaTime);
    }

    private void Start()
    {
        Move();
        btnStop.onClick.AddListener(StopMoveHand);
    }

    void Move()
    {
        hand.DOLocalMove(endPos, 1f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() =>
            {
                hand.DOLocalMove(startPos, 1f)
                .SetEase(Ease.InOutCubic)
                .OnComplete(Move);
            });
    }
    public void StopMoveHand()
    {
        DOTween.Kill(hand);
        MultiResult(hand.GetComponent<RectTransform>());
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
}
