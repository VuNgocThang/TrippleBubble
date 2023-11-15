using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        btnShuffle.onClick.AddListener(LogicGame.instance.Shuffle);
        btnFreeze.onClick.AddListener(LogicGame.instance.Freeze);
    }

    void InitAnim()
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

}
