using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUI : MonoBehaviour
{
    [SerializeField] GameObject heartUI;
    [SerializeField] GameObject starUI;
    [SerializeField] GameObject goldUI;
    [SerializeField] GameObject settingUI;
    [SerializeField] GameObject removeAdsUI;
    [SerializeField] GameObject starCollectorUI;
    [SerializeField] GameObject playUI;
    [SerializeField] GameObject shopUI;
    [SerializeField] GameObject homeUI;
    [SerializeField] GameObject dailyUI;

    private void Start()
    {
        AnimationPopup.instance.DoTween_Button(heartUI, 0, 300, 1f);
        AnimationPopup.instance.DoTween_Button(starUI, 0, 300, 1f);
        AnimationPopup.instance.DoTween_Button(goldUI, 0, 300, 1f);
        AnimationPopup.instance.DoTween_Button(settingUI, 0, 300, 1f);

        AnimationPopup.instance.DoTween_Button(removeAdsUI, -300, 0, 1f);
        AnimationPopup.instance.DoTween_Button(starCollectorUI, -300, 0, 1f);

        AnimationPopup.instance.DoTween_Button(shopUI, 0, -300, 1f);
        AnimationPopup.instance.DoTween_Button(homeUI, 0, -300, 1f);
        AnimationPopup.instance.DoTween_Button(dailyUI, 0, -300, 1f);

        AnimationPopup.instance.AnimScaleZoom(playUI.transform);
    }
}
