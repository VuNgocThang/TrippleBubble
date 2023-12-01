using UnityEngine;
using System;
using System.Collections.Generic;
using System.Reflection;

public class TutorialButtonInGame : MonoBehaviour
{
    public List<GameObject> listItems;
    public GameObject ImageHint;

    private void Start()
    {
        TutHintBtn();
        TutUndo();
    }
    public void Arrange(int index)
    {
        ImageHint.SetActive(true);
        var temp = listItems[index];
        temp.transform.SetSiblingIndex(5);
    }
    public void TutHintBtn()
    {
        if (DataUseInGame.gameData.indexLevel == 1)
        {
            Arrange(0);
        }
    }

    public void TutUndo()
    {
        if (DataUseInGame.gameData.indexLevel == 2)
        {
            LogicGame.instance.isInTut = true;
            Arrange(1);
            //Arrange(2);
        }
    }

    public void TutShuffle()
    {
        var temp = listItems[1];
        temp.transform.SetSiblingIndex(1);

        var temp2 = listItems[2];
        temp2.transform.SetSiblingIndex(2);

        Arrange(3);
    }

    public void TutFreeze()
    {
        var temp = listItems[3];
        temp.transform.SetSiblingIndex(3);

        Arrange(4);
    }


}
