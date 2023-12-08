using UnityEngine;
using System;
using System.Collections.Generic;

public class TutorialButtonInGame : MonoBehaviour
{
    public List<GameObject> listItems;
    public List<GameObject> listHands;
    public List<GameObject> listTexts;

    public GameObject ImageHint;
   
    private void Start()
    {
        TutHintBtn();
        TutUndo();
    }
    public void Arrange(int index)
    {
        ImageHint.SetActive(true);
        listHands[index].SetActive(true);
        listTexts[index].SetActive(true);
        var temp = listItems[index];
        temp.transform.SetSiblingIndex(5);
    }
    public void TutHintBtn()
    {
        if (DataUseInGame.gameData.indexLevel == 1 && !DataUseInGame.gameData.isDaily && !DataUseInGame.gameData.isTutHintDone)
        {
            Arrange(0);
        }
    }

    public void TutUndo()
    {
        if (DataUseInGame.gameData.indexLevel == 2 && !DataUseInGame.gameData.isDaily && !DataUseInGame.gameData.isTutOtherDone)
        {
            LogicGame.instance.isInTut = true;
            Arrange(1);
            //Arrange(2);
        }
    }

    public void TutTrippleUndo()
    {
        var temp = listItems[1];
        temp.transform.SetSiblingIndex(1);

        Arrange(2);
    }

    public void TutShuffle()
    {
        var temp = listItems[2];
        temp.transform.SetSiblingIndex(2);

        Arrange(3);
    }

    public void TutFreeze()
    {
        var temp = listItems[3];
        temp.transform.SetSiblingIndex(3);

        Arrange(4);
    }


}
