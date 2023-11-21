using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    public GameObject star;
    public GameObject level;
    public GameObject timer;
    public GameObject noAds;
    public GameObject pause;

    public TextMeshProUGUI txtStar;
    public TextMeshProUGUI txtLevel;
    public TextMeshProUGUI txtChat;

    public void InitAnim()
    {
        TextChat();
        AnimationPopup.instance.DoTween_Button(star, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(level, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(timer, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(noAds, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(pause, 0, 200, 1f);

        AnimationPopup.instance.MoveAndActiveFalse(txtChat.gameObject, -200, 0, 1f);
    }

    void TextChat()
    {
        if (PlayerPrefs.GetInt("BoosterHint") == 1)
        {
            Debug.Log("USE HINT");
            txtChat.text = "USE HINT";
        }

        if (PlayerPrefs.GetInt("BoosterTimer") == 1)
        {
            Debug.Log("USE TIMER");
            txtChat.text = "USE TIMER";

        }

        if (PlayerPrefs.GetInt("BoosterLightning") == 1)
        {
            Debug.Log("USE LIGHTNING");
            txtChat.text = "USE LIGHTNING";
        }
    }

    private void OnGUI()
    {
        int star = DataUseInGame.gameData.star;
        txtStar.text = star.ToString();

        int indexLevel;
        if (!DataUseInGame.gameData.isDaily)
        {
            indexLevel = DataUseInGame.gameData.indexLevel + 1;
        }
        else
        {
            indexLevel = DataUseInGame.gameData.indexDailyLV + 1;
        }
        txtLevel.text = indexLevel.ToString();
    }

}
