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

    private void Start()
    {
        InitAnim();
    }

    void InitAnim()
    {
        AnimationPopup.instance.DoTween_Button(star, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(level, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(timer, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(noAds, 0, 200, 1f);
        AnimationPopup.instance.DoTween_Button(pause, 0, 200, 1f);
    }

}
