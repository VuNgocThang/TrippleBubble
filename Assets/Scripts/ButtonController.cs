using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    public Button btnReplay;
    public Button btnShuffle;
    public Button btnUndo;
    public Button btnHint;
    public Button btnTrippleUndo;
    public Button btnFreeze;
    void Start()
    {
        btnReplay.onClick.AddListener(ReplayGame);
        btnShuffle.onClick.AddListener(LogicGame.instance.Shuffle);
        btnHint.onClick.AddListener(LogicGame.instance.Hint);
        btnTrippleUndo.onClick.AddListener(LogicGame.instance.UndoTripple);
        btnUndo.onClick.AddListener(LogicGame.instance.Undo);
        btnFreeze.onClick.AddListener(LogicGame.instance.Freeze);
    }

    void ReplayGame()
    {
        DOTween.KillAll();
        SceneManager.LoadScene("SceneGame");
    }
}
