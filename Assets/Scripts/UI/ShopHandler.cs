using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] Button btnGoldBigBundle;
    [SerializeField] Button btnGoldSuperBundle;
    [SerializeField] Button btnGoldMegaBundle;

    private void Start()
    {
        btnGoldBigBundle.onClick.AddListener(() =>
        {
            IncreaseItemBig(1, 900f, 100);
        });

        btnGoldSuperBundle.onClick.AddListener(() =>
        {
            IncreaseItemBig(5, 5400f, 450);
        });

        btnGoldMegaBundle.onClick.AddListener(() =>
        {
            IncreaseItemBig(10, 10800f, 800);
        });


    }
    
    void IncreaseItemBig(int numItem, float numTimer, int goldUse)
    {
        int numHint = DataUseInGame.gameData.numHintItem;
        int numUndo = DataUseInGame.gameData.numUndoItem;
        int numTrippleUndo = DataUseInGame.gameData.numTrippleUndoItem;
        int numShuffle = DataUseInGame.gameData.numShuffleItem;
        int numFreezeTime = DataUseInGame.gameData.numFreezeTimeItem;
        bool isHeartInfinity = DataUseInGame.gameData.isHeartInfinity;
        float timeHeartInfinity = DataUseInGame.gameData.timeHeartInfinity;

        numHint += numItem;
        numUndo += numItem;
        numTrippleUndo += numItem;
        numShuffle += numItem;
        numFreezeTime += numItem;
        isHeartInfinity = true;
        timeHeartInfinity += numTimer;
        GameManager.Instance.SubGold(goldUse);

        DataUseInGame.gameData.numHintItem = numHint;
        DataUseInGame.gameData.numUndoItem = numUndo;
        DataUseInGame.gameData.numTrippleUndoItem = numTrippleUndo;
        DataUseInGame.gameData.numShuffleItem = numShuffle;
        DataUseInGame.gameData.numFreezeTimeItem = numFreezeTime;
        DataUseInGame.gameData.isHeartInfinity = isHeartInfinity;
        DataUseInGame.gameData.timeHeartInfinity = timeHeartInfinity;

        DataUseInGame.instance.SaveData();
    }
}
