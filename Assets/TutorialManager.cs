using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Image handClick;
    int currentStepClick = 0;
    public List<Bubble> listBubbles = new List<Bubble>();
    private void Start()
    {
        AnimHand();
    }
    public void ShowTutorial()
    {
        InitTutorial();
        handClick.gameObject.SetActive(true);
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
                HideHandClick();
            }
        }
    }

    IEnumerator WaitForMoveClick()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 targetPos = listBubbles[currentStepClick].transform.position;

        handClick.transform.DOMove(Camera.main.WorldToScreenPoint(targetPos), 0.4f);
    }

    void HideHandClick()
    {
        handClick.gameObject.SetActive(false);
    }
    
       
   

    void AnimHand()
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
