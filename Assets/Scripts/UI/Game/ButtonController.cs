using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button btnHint;
    public Button btnUndo;
    public Button btnTrippleUndo;
    public Button btnShuffle;
    public Button btnFreeze;

    [SerializeField] TextMeshProUGUI txtNumHint;
    [SerializeField] TextMeshProUGUI txtNumUndo;
    [SerializeField] TextMeshProUGUI txtNumTrippleUndo;
    [SerializeField] TextMeshProUGUI txtNumShuffle;
    [SerializeField] TextMeshProUGUI txtNumFreeze;

    [SerializeField] GameObject lockHint;
    [SerializeField] GameObject lockUndo;
    [SerializeField] GameObject lockTrippleUndo;
    [SerializeField] GameObject lockShuffle;
    [SerializeField] GameObject lockFreeze;

   
    public GameObject handHint;
    public Animation animHint;

    int numHint;
    int numUndo;
    int numTrippleUndo;
    int numShuffle;
    int numFreeze;

    void Start()
    {
        InitAnim();
        btnHint.onClick.AddListener(LogicGame.instance.Hint);
        btnUndo.onClick.AddListener(LogicGame.instance.Undo);
        btnTrippleUndo.onClick.AddListener(LogicGame.instance.UndoTripple);
        btnShuffle.onClick.AddListener(() =>
        {
            LogicGame.instance.useByBtn = true;
            LogicGame.instance.Shuffle();
        });

        btnFreeze.onClick.AddListener(LogicGame.instance.Freeze);
    }
    public void InitButton()
    {
        btnHint.interactable = false;
        btnUndo.interactable = false;
        btnTrippleUndo.interactable = false;
        btnShuffle.interactable = false;
        btnFreeze.interactable = false;

        if (DataUseInGame.gameData.indexLevel >= 2 || DataUseInGame.gameData.isDaily)
        {
            lockUndo.SetActive(false);
            btnUndo.interactable = true;
            lockTrippleUndo.SetActive(false);
            btnTrippleUndo.interactable = true;
            lockFreeze.SetActive(false);
            btnFreeze.interactable = true;
            lockShuffle.SetActive(false);
            btnShuffle.interactable = true;
        }

        if (DataUseInGame.gameData.indexLevel >= 1 || DataUseInGame.gameData.isDaily)
        {
            lockHint.SetActive(false);
            btnHint.interactable = true;
        }


    }
    public void InitAnim()
    {
        AnimationPopup.instance.DoTween_Button(btnHint.gameObject, 0, -200, 1f);
        AnimationPopup.instance.DoTween_Button(btnUndo.gameObject, 0, -200, 1f);
        AnimationPopup.instance.DoTween_Button(btnTrippleUndo.gameObject, 0, -200, 1f);
        AnimationPopup.instance.DoTween_Button(btnShuffle.gameObject, 0, -200, 1f);
        AnimationPopup.instance.DoTween_Button(btnFreeze.gameObject, 0, -200, 1f);
    }

    private void OnGUI()
    {
        numHint = DataUseInGame.gameData.numHintItem;
        numUndo = DataUseInGame.gameData.numUndoItem;
        numTrippleUndo = DataUseInGame.gameData.numTrippleUndoItem;
        numShuffle = DataUseInGame.gameData.numShuffleItem;
        numFreeze = DataUseInGame.gameData.numFreezeTimeItem;

        txtNumHint.text = numHint.ToString();
        txtNumUndo.text = numUndo.ToString();
        txtNumTrippleUndo.text = numTrippleUndo.ToString();
        txtNumShuffle.text = numShuffle.ToString();
        txtNumFreeze.text = numFreeze.ToString();
    }

    public void SetStateIfContinue()
    {
        btnHint.interactable = false;
        btnUndo.interactable = false;
        btnTrippleUndo.interactable = false;
        btnShuffle.interactable = false;
        btnFreeze.interactable = false;
    }

    public void SetStateAfterContinueDone()
    {
        btnHint.interactable = true;
        if(DataUseInGame.gameData.indexLevel > 1 || DataUseInGame.gameData.isDaily)
        {
            btnUndo.interactable = true;
            btnTrippleUndo.interactable = true;
            btnShuffle.interactable = true;
            btnFreeze.interactable = true;
        }
    }
}
