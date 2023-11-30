using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image handClick;
    public Image handRotate;
    int currentStepClick = 0;
    bool isDone;
    public List<Bubble> listBubbles = new List<Bubble>();
    public List<int> listIndex = new List<int>()
    {
        0,0,0,2,4,4,2,4,2
    };


    private void Update()
    {
        AnimHandRotate();

    }
    public void ShowTutorial()
    {
        InitTutorial();
        MoveHandClick();
    }

    void InitTutorial()
    {
        foreach (Bubble bb in LogicGame.instance.listBB)
        {
            listBubbles.Add(bb);
        }
    }

    void MoveHandClick()
    {
        StartCoroutine(WaitForMoveClick());
    }

    public void OnClick(Bubble bb)
    {
        if (bb.gameObject.transform.position == listBubbles[currentStepClick].transform.position)
        {
            currentStepClick++;
            if (currentStepClick < 3)
            {
                MoveHandClick();
            }
            else
            {
                StartCoroutine(HideHandClick());
            }
        }
    }

    IEnumerator WaitForMoveClick()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 targetPos = listBubbles[currentStepClick].transform.position;
        Debug.Log("hmm");
        handClick.transform.DOMove(Camera.main.WorldToScreenPoint(targetPos), 0.4f);
    }

    IEnumerator HideHandClick()
    {
        handClick.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        handRotate.gameObject.SetActive(true);
        GameManager.Instance.canRotate = true;
    }

    public void AnimHandRotate()
    {
        if (handRotate.gameObject != null && !isDone)
        {
            isDone = true;
            handRotate.gameObject.SetActive(false);
        }
    }

    public void AnimHand()
    {
        if (handClick.gameObject != null)
        {
            handClick.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 1f)
                .OnComplete(() =>
                {
                    handClick.transform.DOScale(new Vector3(1f, 1f, 1f), 1f)
                    .OnComplete(() =>
                    {
                        AnimHand();
                    });
                });
        }
    }
}
