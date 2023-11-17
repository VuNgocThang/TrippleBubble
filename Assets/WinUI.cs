
using TMPro;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    public TextMeshProUGUI txtPoint;
    public TextMeshProUGUI txtPointMulti;

    private void OnGUI()
    {
        int star = 100 + (DataUseInGame.gameData.indexLevel + 1) * 20 + Mathf.RoundToInt(LogicGame.instance.timer.timeLeft) * 2;
        txtPoint.text = star.ToString();
        txtPointMulti.text = star.ToString();
    }
}
